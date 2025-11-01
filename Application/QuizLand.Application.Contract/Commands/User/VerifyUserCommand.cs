using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.User;

public class VerifyUserCommand : ICommand
{
    public string PhoneNumber { get; set; }
    public string Otp { get; set; }
}