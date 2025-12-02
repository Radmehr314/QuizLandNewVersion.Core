namespace QuizLand.Application.Contract.QueryResults.Round;

public class GetRoundInfoQueryResult
{
    public long Id { get; set; }
    public long? CourseId { get; set; }
    public string? CourseName { get; set; }
}