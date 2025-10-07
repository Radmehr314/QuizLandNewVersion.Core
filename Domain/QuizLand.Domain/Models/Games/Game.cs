using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Rands;
using QuizLand.Domain.Models.Users;

namespace QuizLand.Domain.Models.Games;

public class Game : BaseEntity<Guid>
{
    public int Type { get; set; }
    //1 = 2 players
    //2 = 1 player
    //3 = 2 player with a friend
    public int CountOfJoinedClients { get; set; }
    //How Many Person Joind Now
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public Guid? WinnerUserId { get; set; }
    public bool MatchClients { get; set; } =  false;
    public User Winner { get; set; }

    public IEnumerable<Gamer> Gamers { get; set; }

    /*public ICollection<Rand> Rands { get; set; }*/
}