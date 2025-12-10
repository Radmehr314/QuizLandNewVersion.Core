using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.GameRequest;

public class SendNewGameRequestCommand : ICommand
{
    public Guid userId { get; set; }
}