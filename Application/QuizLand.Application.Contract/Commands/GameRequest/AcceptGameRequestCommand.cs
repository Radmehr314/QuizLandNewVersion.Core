using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.GameRequest;

public class AcceptGameRequestCommand : ICommand
{
    public long RequestId { get; set; }
}