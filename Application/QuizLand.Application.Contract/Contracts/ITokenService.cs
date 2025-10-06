namespace QuizLand.Application.Contract.Contracts;

public interface ITokenService
{
    AccessToken Generate(Guid userId , long tokenVersion, string deviceId);
    AccessToken GenerateSupportToken(Guid userId);

   
}

public sealed record AccessToken(string Value, DateTime ExpiresAt)
{
    public TimeSpan ExpiresIn => ExpiresAt - DateTime.UtcNow;
}