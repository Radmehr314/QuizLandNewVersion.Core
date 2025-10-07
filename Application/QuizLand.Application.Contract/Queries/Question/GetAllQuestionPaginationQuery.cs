using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Question;

public class GetAllQuestionPaginationQuery : IQuery
{
    public int size { get; set; }
    public int pageNumber { get; set; }
    public long CourseId { get; set; }
}