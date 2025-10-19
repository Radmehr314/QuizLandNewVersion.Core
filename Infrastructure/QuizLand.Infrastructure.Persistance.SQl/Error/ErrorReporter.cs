using QuizLand.Domain.Models.ErrorLogs;
using System;
using Microsoft.Extensions.DependencyInjection;
using QuizLand.Application.Contract.Commands.ErrorLog;
using QuizLand.Application.Contract.Contracts;

namespace QuizLand.Infrastructure.Persistance.SQl.Error;

public class ErrorReporter : IErrorReporter
{
    private readonly DataBaseContext _db;
    private readonly IErrorNotifyQueue _queue;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ITelegramNotifier _telegramNotifier;

    public ErrorReporter(DataBaseContext db, IErrorNotifyQueue queue, IServiceScopeFactory scopeFactory, ITelegramNotifier telegramNotifier)
    {
        _db = db;
        _queue = queue;
        _scopeFactory = scopeFactory;
        _telegramNotifier = telegramNotifier;
    }

    private static string? Clip(string? s, int max)
        => string.IsNullOrEmpty(s) ? s : (s.Length <= max ? s : s[..max]);

    public async Task ReportAsync(AddErrorLogCommand report)
    {
        // 1) نوشتن در DB با اسکوپ جدید
        using (var scope = _scopeFactory.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            db.ErrorLogs.Add(new ErrorLog()
            {
                Level        = report.Level,
                Message      = report.Message,
                Details      = report.Details,
                Path         = report.Path,
                Method       = report.Method,
                StatusCode   = report.StatusCode,
                UserId       = report.UserId,
                ClientIp     = report.ClientIp,
                UserAgent    = report.UserAgent,
                CorrelationId= report.CorrelationId,
            });
            await db.SaveChangesAsync();
        }


        await _telegramNotifier.SendAsync(
            level: report.Level,
            message: report.Message,
            path: report.Path,
            statusCode: report.StatusCode,
            correlationId: report.CorrelationId,
            userId:report.UserId,
            whenUtc: DateTime.UtcNow);
    }

}