using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.RoundQuestionAnswers;

public class SubmitRoundQuestionAnswersCommand : ICommand
{
    public Guid GameId { get; init; }
    public int  RoundNumber { get; init; }
    public List<RoundAnswerItem> Answers { get; init; } = new();
}

public sealed class RoundAnswerItem
{
    public long RoundQuestionId { get; init; }
    public int SelectedOption { get; init; }
}
