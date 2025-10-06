using Microsoft.EntityFrameworkCore;
using QuizLand.Infrastructure.Persistance.SQl.FrameWork;
using QuizLand.Domain.Models.TicketMessages;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class TicketMessgeRepository : ITicketMessageRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public TicketMessgeRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task Add(TicketMessage ticketMessage) => await _dataBaseContext.TicketMessages.AddAsync(ticketMessage);

    public async Task<TicketMessage> GetById(long ticketMessageId) => await _dataBaseContext.TicketMessages.FirstOrDefaultAsync(f=>f.Id == ticketMessageId);

    public async Task<List<TicketMessage>> GetByTicketId(long ticketId) => await _dataBaseContext.TicketMessages.Where(f=>f.Ticketid == ticketId).ToListAsync();
    public async Task<List<TicketMessage>> All() => await _dataBaseContext.TicketMessages.ToListAsync();

    public async Task<List<TicketMessage>> AllPagination(int pageNumber, int size) => await _dataBaseContext.TicketMessages.Skip(Helper.CalculateSkip(pageNumber, size)).Take(size).ToListAsync();
}