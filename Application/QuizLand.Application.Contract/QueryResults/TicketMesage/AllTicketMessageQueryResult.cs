using QuizLand.Domain.Models.TicketMessages;

namespace QuizLand.Application.Contract.QueryResults.TicketMesage;

public class AllTicketMessageQueryResult
{
    public long Id { get; set; }
    public long TicketId { get; set; }
    public string Username { get; set; }
    public string Visibility { get; set; }
    public string Body { get; set; }
    public string SentAt { get; set; }
    public long? ReplyTo { get; set; }
    public bool IsSupporter { get; set; }
    public Guid UserId { get; set; }
}