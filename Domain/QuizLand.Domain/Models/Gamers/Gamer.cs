using System.Runtime.InteropServices.JavaScript;
using QuizLand.Domain.Models.Games;
using QuizLand.Domain.Models.RandQuestionAnswers;
using QuizLand.Domain.Models.Users;

namespace QuizLand.Domain.Models.Gamers;

public class Gamer : BaseEntity<Guid>
{
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    public bool IsOwner { get; set; }
    public DateTime JoinedAt { get; set; }
    public Game Game { get; set; }
    public User User { get; set; }
    /*
    public IEnumerable<RandQuestionAnswer> RandQuestionAnswers { get; set; }
*/
}