using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Course;

public class GetCourseByIdQuery:IQuery
{
    public long Id { get; set; }
}