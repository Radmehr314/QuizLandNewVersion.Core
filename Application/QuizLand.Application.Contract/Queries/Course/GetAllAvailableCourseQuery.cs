using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Course;

public class GetAllAvailableCourseQuery : IQuery
{
    public Guid GameId { get; set; }
}