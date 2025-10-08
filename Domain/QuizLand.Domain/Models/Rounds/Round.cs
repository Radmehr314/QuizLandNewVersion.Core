using QuizLand.Domain.Models.Courses;
using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;
using QuizLand.Domain.Models.RandQuestions;

namespace QuizLand.Domain.Models.Rands;

public class Round :  BaseEntity<long>
{
    public Guid SelectingGamerId { get; set; }
    public Guid FirstAnswerGamerId { get; set; }
    public Guid GameId { get; set; }
    public long? CourseId { get; set; }
    public int RoundNumber { get; set; } //Round  1 / 2 / 3 / 4
    public RoundStatus RoundStatus { get; set; }
    public long? FirstRandQuestionId { get; set; }
    public long? SecondRandQuestionId { get; set; }
    public long? ThirdRandQuestionId { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    
    public Gamer Gamer { get; set; }
    public Gamer FirstAnswerGamer { get; set; }
    public Game Game { get; set; }
    public RoundQuestion? FirstRoundQuestion { get; set; }
    public RoundQuestion? SecondRoundQuestion { get; set; }
    public RoundQuestion? ThirdRoundQuestion { get; set; }
    public Course? Course { get; set; }
}

public enum RoundStatus
{ 
    PendingCourse = 0,
    AwaitingP1 = 1,
    AwaitingP2 = 2,
    Completed = 3,
}
