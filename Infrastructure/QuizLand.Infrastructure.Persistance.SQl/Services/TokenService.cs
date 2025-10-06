using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizLand.Application.Contract.Contracts;

namespace QuizLand.Infrastructure.Persistance.SQl.Services;

public class TokenService : ITokenService
{
    
    public AccessToken Generate(Guid userId, long tokenVersion, string deviceId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("67a91ced-20cb-4b43-86dd-8a6f58d27f6f"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var now   = DateTime.UtcNow;
        var exp   = now.AddMinutes(20);

        var claims = new[]
        {
            new Claim("sub", userId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim("tver", tokenVersion.ToString()),
            new Claim("did", deviceId),
            new Claim("jti", Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken("https://localhost:7055/", "https://localhost:5232/", claims, now, exp, creds);
        var value = new JwtSecurityTokenHandler().WriteToken(token);
        return new AccessToken(value, exp);
    }

    public  AccessToken GenerateSupportToken(Guid userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("67a91ced-20cb-4b43-86dd-8a6f58d27f6f"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var now   = DateTime.UtcNow;
        var exp   = now.AddMinutes(20);

        var claims = new[]
        {
            new Claim("sub", userId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, "Supporter") 
        };

        var token = new JwtSecurityToken("https://localhost:7055/", "https://localhost:5232/", claims, now, exp, creds);
        var value = new JwtSecurityTokenHandler().WriteToken(token);
        return new AccessToken(value, exp);
    }
}