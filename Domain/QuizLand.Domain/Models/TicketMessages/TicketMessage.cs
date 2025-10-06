using QuizLand.Domain.Models.Supporters;
using QuizLand.Domain.Models.Tickets;
using QuizLand.Domain.Models.Users;

namespace QuizLand.Domain.Models.TicketMessages;

public class TicketMessage : BaseEntity<long>
{
    public long Ticketid { get; set; }
    public Guid? SenderUserId { get; set; }
    public Guid? SenderSupporterId { get; set; }
    public Visibility Visibility { get; set; }
    public string Body { get; set; }
    public DateTime SentAt { get; set; }
    public long? ReplyTo { get; set; }
    public Ticket Ticket { get; set; }
    public bool IsSupporter { get; set; }
    public User SenderUser { get; set; }
    public Supporter SenderSupporter { get; set; }
}

public enum Visibility
{
    InternalNote = 0,
    Public = 1,
}

