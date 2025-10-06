using System.Net;
using System.Text;
using System.Text.Json;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using QuizLand.Framework.Minimal.Security;
using QuizLand.Api.Framework;
using QuizLand.Api.Hubs;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Exceptions;

using QuizLand.Infrastructure.Config;
using QuizLand.Infrastructure.Persistance.SQl;


var builder = WebApplication.CreateBuilder(args);

// اگر داری با پروفایل Development اجرا می‌کنی، همین الان از appsettings.* میاد
var pepper = builder.Configuration["Security:Pepper"];

// Contracts → Implementations (زیرساخت)
builder.Services.AddDistributedMemoryCache();



// JWT
var signingKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]!));


builder.Services.AddCors(options =>
{
    options.AddPolicy("SpaLocal", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5500",
                "http://127.0.0.1:5500",
                "https://localhost:5500",
                "https://127.0.0.1:5500",
                "https://192.168.15.17", // اضافه کردن این‌ها به WithOrigins
                "http://192.168.15.17",
                "https://192.168.15.17:5005",
                "http://192.168.15.17:5005",
                "https://192.168.15.4:5005",
                "http://192.168.15.4:5005",
                "http://192.168.15.4"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // اجازه ارسال کوکی‌ها
    });
});



// Add services to the container.

builder.Services.AddControllers(a =>
{
    a.Conventions.Add(new CqrsModelConvention());
});

builder.Services.AddControllers();

builder.Services.AddSignalR().AddJsonProtocol();
builder.Services.AddScoped<IRealTimeNotifier, SignalRNotifier>();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacModule(builder.Configuration.GetConnectionString("DbConnection"))));
Host.CreateDefaultBuilder(args).UseServiceProviderFactory(new AutofacServiceProviderFactory());
var autofac = new ContainerBuilder();
autofac.RegisterModule(new AutofacModule(builder.Configuration.GetConnectionString("DbConnection")));


//sql
builder.Services.AddDbContextPool<DataBaseContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
//end sql

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = 1024 * 1024 * 100; // 100 MB
    options.MultipartBodyLengthLimit = 1024 * 1024 * 100; // 100MB
});
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = builder.Configuration.GetValue<long>("Kestrel:Limits:MaxRequestBodySize");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "SAMT New Version API", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30),
            
            
            NameClaimType = System.Security.Claims.ClaimTypes.NameIdentifier

        };
        o.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                var accessToken = ctx.Request.Query["access_token"];
                var path = ctx.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    path.StartsWithSegments("/hubs/notifications"))
                {
                    ctx.Token = accessToken;
                }
                return Task.CompletedTask;
            },

            OnChallenge = context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                var errorJson = JsonSerializer.Serialize(new
                {
                    ErrorCode = "ERR_401",
                    ErrorMessage = "دسترسی غیرمجاز. لطفاً وارد شوید."
                });
                return context.Response.WriteAsync(errorJson);
            }
        };
    });




builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    try
    {
        DatabaseInitializer.Initialize(app.Services);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "یک خطا در ایجاد یا مایگریت دیتابیس رخ داد.");
    }
}

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        response.ContentType = "application/json";
        await response.WriteAsync(JsonSerializer.Serialize(new
        {
            ErrorCode = "ERR_401",
            ErrorMessage = "دسترسی غیرمجاز. لطفاً وارد شوید."
        }));
    }
});




app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        // تنظیم پیش‌فرض
        var errorCode = "ERR_500";
        var errorMessage = "یک خطای ناشناس رخ داده ، با پشتیبانی تماس بگیرید!!!";
        var statusCode = HttpStatusCode.InternalServerError;


        
        // مدیریت خطاهای خاص
        if (exception is NotFoundException)
        {
            errorCode = "ERR_404";
            errorMessage = exception.Message;
            statusCode = HttpStatusCode.NotFound;
        }
        else if (exception is UserAccessException)
        {
            errorCode = "ERR_403";
            errorMessage = exception.Message;
            statusCode = HttpStatusCode.Forbidden;
        }
        else if (exception is ValidationException validationException)
        {
            errorCode = "ERR_400";
            errorMessage = string.Join(", ", validationException.Errors);
            statusCode = HttpStatusCode.BadRequest;
        }
        
        // تنظیم کد وضعیت HTTP
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        // ارسال پاسخ به کلاینت
        await context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            ErrorCode = errorCode,
            ErrorMessage = errorMessage
        }));
    });
});

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
    app.UseSwagger();
    app.UseSwaggerUI();
/*}*/


// چکِ ساده
if (string.IsNullOrWhiteSpace(pepper))
    throw new InvalidOperationException("Missing Security:Pepper");



app.UseCors("SpaLocal");
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health");

/*
app.UseHttpsRedirection();
*/

app.MapControllers();
app.MapHub<NotificationHub>("/hubs/notifications");

app.Run();

