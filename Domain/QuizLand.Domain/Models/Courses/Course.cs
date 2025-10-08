using QuizLand.Domain.Models.Questions;
using QuizLand.Domain.Models.Rands;

namespace QuizLand.Domain.Models.Courses;

public class Course : BaseEntity<long>
{
    public string Title { get; set; }
    public IEnumerable<Round> Rounds { get; set; }
    public IEnumerable<Question> Questions { get; set; }
}