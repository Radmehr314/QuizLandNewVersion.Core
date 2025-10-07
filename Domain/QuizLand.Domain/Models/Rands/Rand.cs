using QuizLand.Domain.Models.Courses;
using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;
using QuizLand.Domain.Models.RandQuestions;

namespace QuizLand.Domain.Models.Rands;

public class Rand :  BaseEntity<Guid>
{
    public Guid GamerClientId { get; set; }
    public Guid GameId { get; set; }
    public long CourseId { get; set; }
    public Guid FirstRandQuestionId { get; set; }
    public Guid SecondRandQuestionId { get; set; }
    public Guid ThirdRandQuestionId { get; set; }
    /*public Gamer Gamer { get; set; }
    public Game Game { get; set; }*/
    public RandQuestion FirstRandQuestion { get; set; }
    public RandQuestion SecondRandQuestion { get; set; }
    public RandQuestion ThirdRandQuestion { get; set; }
    public Course Course { get; set; }
}