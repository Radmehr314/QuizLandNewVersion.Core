
using QuizLand.Domain.Models.Avatars;
using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;
using QuizLand.Domain.Models.Provinces;
using QuizLand.Domain.Models.TicketMessages;
using QuizLand.Domain.Models.Tickets;

namespace QuizLand.Domain.Models.Users;

public class User:BaseEntity<Guid>
{
 
    public string Username { get; set; }
    public long? AvatarId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public long XP { get; set; }
    /*
    public long Level { get; set; }
    */
    public long Coin { get; set; }
    public string PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
    public bool IsOnline { get; set; }  = false;
    public string? IP { get; set; }
    public string ActiveDeviceId { get; set; }
    public long TokenVersion { get; set; } = 1;
    public bool IsBan { get; set; } = false;
    public bool IsVerified { get; set; }  = true;
    public Avatar? Avatar { get; set; }
    public IEnumerable<Game> Games { get; set; }
    public IEnumerable<Game> MyTurnGames { get; set; }
    public IEnumerable<Gamer> Gamers { get; set; }
    public IEnumerable<TicketMessage> TicketMessages { get; set; }
    
    public IEnumerable<Ticket> Tickets { get; set; }

    public long ProvinceId { get; set; }
    public Province Province { get; set; }
}
