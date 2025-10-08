using QuizLand.Domain.Models.Questions;
using QuizLand.Domain.Models.RandQuestionAnswers;
using QuizLand.Domain.Models.Rands;

namespace QuizLand.Domain.Models.RandQuestions;

public class RoundQuestion : BaseEntity<Guid>
{
    public long QuestionId { get; set; }
    public int  QuestionNumber { get; set; } //Question 1 2 3 
    public long RoundId { get; set; }
    public Round Round { get; set; }
    public Question Question { get; set; }
    public IEnumerable<RoundQuestionAnswer> RoundQuestionAnswers { get; set; }
}