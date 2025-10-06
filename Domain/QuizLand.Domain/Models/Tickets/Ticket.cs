using QuizLand.Domain.Models.Supporters;
using QuizLand.Domain.Models.TicketMessages;
using QuizLand.Domain.Models.Users;

namespace QuizLand.Domain.Models.Tickets;

public class Ticket : BaseEntity<long>
{
    public Guid UserId { get; set; }
    public string Subject { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public Priority Priority { get; set; }
    public Guid? AssigneeSupporterId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastActivityAt { get; set; }
    public bool IsLocked { get; set; } = false;
    public Supporter? Supporter { get; set; }
    public User User { get; set; }
    public IEnumerable<TicketMessage> TicketMessages { get; set; }
}

public enum TicketStatus
{
    Open = 0,
    Pending = 1,
    WaitingForCustomer = 2,
    Resolved = 3,
    Closed = 4,
    Reopened = 5,
    Rejected = 6,
}
public enum Priority
{
    Low = 0,
    Normal = 1,
    High = 2,
    Urgent = 3,
}
