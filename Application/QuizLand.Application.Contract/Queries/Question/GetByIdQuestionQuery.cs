using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Question;

public class GetByIdQuestionQuery : IQuery
{
    public long Id { get; set; }
}