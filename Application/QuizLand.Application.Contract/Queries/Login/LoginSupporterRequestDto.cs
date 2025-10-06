using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Login;

public class LoginSupporterRequestDto : IQuery
{
    public string Username { get; set; }
    public string Password { get; set; }
}