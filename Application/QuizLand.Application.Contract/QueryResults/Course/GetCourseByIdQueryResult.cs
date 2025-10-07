using QuizLand.Application.Contract.QueryResults.Question;

namespace QuizLand.Application.Contract.QueryResults.Course;

public class GetCourseByIdQueryResult
{
    public long Id { get; set; }
    public string Title { get; set; }
    public IEnumerable<GetAllQuestionQueryResult> GetAllQuestionQueryResults { get; set; }
}