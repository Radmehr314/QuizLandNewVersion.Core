using QuizLand.Application.Contract.QueryResults.TicketMesage;
using QuizLand.Domain.Models.Tickets;

namespace QuizLand.Application.Contract.QueryResults.Ticket;

public class GetTicketByIdQueryResult
{
    public long Id { get; set; }
    public string Subject { get; set; }
    public string UserUsername { get; set; }
    public Guid UserId { get; set; }
    public string TicketStatus { get; set; }
    public string Priority { get; set; }
    public string CreatedAt { get; set; }
    public string LastActivityAt { get; set; }
    public bool IsAssigned { get; set; }
    public bool IsLock { get; set; }
    public IEnumerable<AllTicketMessageQueryResult> AllTicketMessageQueryResults { get; set; }
}