using Microsoft.EntityFrameworkCore;
using QuizLand.Infrastructure.Persistance.SQl.FrameWork;
using QuizLand.Domain.Models.Tickets;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public TicketRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task Add(Ticket ticket) => await _dataBaseContext.Tickets.AddAsync(ticket);

    public async Task<Ticket> GetById(long ticketId) => await _dataBaseContext.Tickets.Where(t => t.Id == ticketId)
            .Include(t => t.User)
            .Include(t => t.Supporter)
            .Include(t => t.TicketMessages)
            .ThenInclude(m => m.SenderUser)
            .Include(t => t.TicketMessages)
            .ThenInclude(m => m.SenderSupporter)
            .Include(t => t.TicketMessages)
            .FirstOrDefaultAsync();

    
    public async Task<List<Ticket>> All() => await _dataBaseContext.Tickets.ToListAsync();

    public async Task<List<Ticket>> AllPagination(int pageNumber, int size) => await _dataBaseContext.Tickets.Include(f=>f.User).Include(f=>f.Supporter).Skip(Helper.CalculateSkip(pageNumber, size)).Take(size).ToListAsync();
    public async Task<long> Count() => await _dataBaseContext.Tickets.LongCountAsync();
}