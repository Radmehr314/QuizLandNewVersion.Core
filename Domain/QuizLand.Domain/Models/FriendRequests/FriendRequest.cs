using QuizLand.Domain.Models.Users;

namespace QuizLand.Domain.Models.FriendRequests;

public class FriendRequest : BaseEntity<long>
{
    public Guid RequesterId { get; set; }
    public Guid ReceiverId { get; set; }
    public FriendRequestStatus FriendRequestStatus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? RespondedAt { get; set; } = DateTime.Now;
    
    public User Requester { get; set; }
    public User Receiver { get; set; }
}

public enum FriendRequestStatus
{ 
    Pending = 0,
    Accepted = 1,
    Rejected = 2,
    Canceled = 3,
}
