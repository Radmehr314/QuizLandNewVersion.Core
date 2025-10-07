namespace QuizLand.Application.Contract.QueryResults.Course;

public class GetAllCoursePaginationQueryResult
{
    public List<GetAllCourseQueryResult> AllCourseQueryResults { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
}