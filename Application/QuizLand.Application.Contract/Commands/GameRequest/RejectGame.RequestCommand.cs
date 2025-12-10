using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.GameRequest;

public class RejectGameRequestCommand : ICommand
{
    public long RequestId { get; set; }
}