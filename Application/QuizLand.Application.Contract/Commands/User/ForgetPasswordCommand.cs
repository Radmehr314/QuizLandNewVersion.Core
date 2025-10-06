using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.User;

public class ForgetPasswordCommand : ICommand
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Otp { get; set; }
    public string IP { get; set; }
    public string DeviceId { get; set; }
}