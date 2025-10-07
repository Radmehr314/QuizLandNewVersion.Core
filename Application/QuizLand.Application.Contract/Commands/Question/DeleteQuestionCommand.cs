using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.Question;

public class DeleteQuestionCommand : ICommand
{
    public long Id { get; set; }
}