using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.Supporter;

public class SupporterStatusCommand : ICommand
{
    public Guid Id { get; set; }
}