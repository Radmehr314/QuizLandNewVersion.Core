using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.RandQuestions;

namespace QuizLand.Domain.Models.RandQuestionAnswers;

public class RoundQuestionAnswer : BaseEntity<Guid>
{
    public long RoundQuestionId { get; set; }
    public Guid GamerId { get; set; }
    public int SelectedOption { get; set; } //option 1  or 2 or 3 or 4  cliecked 
    public Gamer Gamer { get; set; }
    public RoundQuestion RoundQuestion { get; set; }
    public bool IsCorrect { get; set; }
    public DateTime SubmitedAt { get; set; }
}