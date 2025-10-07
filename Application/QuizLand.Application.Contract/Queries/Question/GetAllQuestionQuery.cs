using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Question;

public class GetAllQuestionQuery : IQuery
{
    public long CourseId { get; set; }
}