using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Course;

public class GetAllCoursePaginationQuery : IQuery
{
    public int size { get; set; }
    public int pageNumber { get; set; }
}