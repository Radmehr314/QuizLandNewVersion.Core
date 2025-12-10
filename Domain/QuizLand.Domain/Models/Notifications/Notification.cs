using QuizLand.Domain.Models.Users;

namespace QuizLand.Domain.Models.Notifications;

public class Notification :  BaseEntity<long>
{
    public Guid UserId { get; set; }
    public NotificationTypes NotificationTypes { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public bool IsSeen { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public User User { get; set; }
}

public enum NotificationTypes
{ 
    FriendRequest = 0,
    AcceptFriendRequest = 1,
    RejectAcceptFriendRequest = 2,
    GameRequest = 3,
    AcceptGameRequest = 4,
    RejectGameRequest = 5,
}
