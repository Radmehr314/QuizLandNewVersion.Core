namespace QuizLand.Application.Contract.QueryResults.Login;

public class LoginDto
{
    public string AccessToken { get; init; } = default!;
    public string TokenType   { get; init; } = "Bearer";
    public int    ExpiresIn   { get; init; }
    
}