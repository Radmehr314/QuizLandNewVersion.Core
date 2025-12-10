using QuizLand.Domain.Models.Users;

namespace QuizLand.Domain.Models.GameRequests;

public class GameRequest :  BaseEntity<long>
{
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? RespondedAt { get; set; } = DateTime.Now;
    public GameRequestStatus GameRequestStatus { get; set; }
    public User FirstUser { get; set; }
    public User SecondUser { get; set; }
}

public enum GameRequestStatus
{ 
    Pending = 0,
    Accepted = 1,
    Rejected = 2,
    Canceled = 3,
}
