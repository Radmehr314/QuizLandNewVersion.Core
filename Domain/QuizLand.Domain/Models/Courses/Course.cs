using QuizLand.Domain.Models.Questions;

namespace QuizLand.Domain.Models.Courses;

public class Course : BaseEntity<long>
{
    public string Title { get; set; }
    public IEnumerable<Question> Questions { get; set; }
}