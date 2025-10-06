using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.Supporter;

public class AddSupporterCommand : ICommand
{
    public string Username { get; set; }
    public string Password { get; set; }
}