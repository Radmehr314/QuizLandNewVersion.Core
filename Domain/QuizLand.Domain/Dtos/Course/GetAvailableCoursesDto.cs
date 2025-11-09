namespace QuizLand.Domain.Dtos.Course;

public class GetAvailableCoursesDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public bool IsAvailable { get; set; }
}