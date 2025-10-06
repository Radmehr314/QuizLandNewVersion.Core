using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using QuizLand.Application.Contract.Contracts;
using Microsoft.AspNetCore.Http;


namespace QuizLand.Infrastructure.Persistance.SQl.Services;

public class UserInfoService : IUserInfoService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserInfoService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Guid GetUserIdByToken() 
    {
        var tokenStr = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
        if (tokenStr == null) return new();
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("67a91ced-20cb-4b43-86dd-8a6f58d27f6f");
        tokenHandler.ValidateToken(tokenStr, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        }, out var validatedToken);
        var jwtToken = (JwtSecurityToken)validatedToken;
        return jwtToken.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c =>Guid.Parse(c.Value)).FirstOrDefault();
    }
}