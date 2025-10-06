using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Login;

public class LoginRequestDto : IQuery
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ActiveDeviceId { get; set; }

}