namespace QuizLand.Domain.Models.TicketMessages;

public interface ITicketMessageRepository
{
    Task Add(TicketMessage ticketMessage);
    Task<TicketMessage> GetById(long ticketMessageId);
    Task<List<TicketMessage>> GetByTicketId(long ticketId);
    Task<List<TicketMessage>> All();
    Task<List<TicketMessage>> AllPagination(int pageNumber, int size);
}