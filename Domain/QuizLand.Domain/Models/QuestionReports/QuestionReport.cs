using QuizLand.Domain.Models.Questions;
using QuizLand.Domain.Models.Users;

namespace QuizLand.Domain.Models.QuestionReports;

public class QuestionReport : BaseEntity<long>
{
    public long QuestionId { get; set; }
    public ReportType ReportType { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Question Question { get; set; }

}

public enum ReportType
{ 
    WrongCourse = 0,
    WrongQuestion = 1,
    WrongAnswer = 2,
    Other = 3,
}