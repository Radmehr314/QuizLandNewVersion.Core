using QuizLand.Domain.Models.Questions;
using QuizLand.Domain.Models.RandQuestionAnswers;

namespace QuizLand.Domain.Models.RandQuestions;

public class RandQuestion : BaseEntity<Guid>
{
    public long QuestionId { get; set; }
    public int CountRand { get; set; }
    public Question Question { get; set; }
    public IEnumerable<RandQuestionAnswer> RandQuestionAnswers { get; set; }
}