namespace QuizLand.Domain.Models.Tickets;

public interface ITicketRepository
{
    Task Add(Ticket ticket);
    Task<Ticket> GetById(long ticketId);
    Task<List<Ticket>> All();
    Task<List<Ticket>> AllPagination(int pageNumber, int size);
    Task<long> Count();

}