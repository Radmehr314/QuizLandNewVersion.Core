using QuizLand.Domain.Models.TicketMessages;
using QuizLand.Domain.Models.Tickets;

namespace QuizLand.Domain.Models.Supporters;

public class Supporter : BaseEntity<Guid>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public bool IsBan { get; set; } = false;
    
    public IEnumerable<Ticket> Tickets { get; set; }
    public IEnumerable<TicketMessage> TicketMessages { get; set; }


}