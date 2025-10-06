using QuizLand.Domain;
using QuizLand.Domain.Models.CodeLogs;
using QuizLand.Domain.Models.Supporters;
using QuizLand.Domain.Models.TicketMessages;
using QuizLand.Domain.Models.Tickets;
using QuizLand.Domain.Models.Users;

namespace QuizLand.Infrastructure.Persistance.SQl;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataBaseContext _dataBaseContext;
    
    public IUserRepository UserRepository { get; set; }
    public ICodeLogsRepository CodeLogsRepository { get; set; }
    public ISupporterRepository SupporterRepository { get; set; }
    public ITicketRepository TicketRepository { get; set; }
    public ITicketMessageRepository TicketMessageRepository { get; set; }

    public UnitOfWork(DataBaseContext dataBaseContext, IUserRepository userRepository, ICodeLogsRepository codeLogsRepository, ISupporterRepository supporterRepository, ITicketRepository ticketRepository, ITicketMessageRepository ticketMessageRepository)
    {
        _dataBaseContext = dataBaseContext;
        UserRepository = userRepository;
        CodeLogsRepository = codeLogsRepository;
        SupporterRepository = supporterRepository;
        TicketRepository = ticketRepository;
        TicketMessageRepository = ticketMessageRepository;
    }
    
    public void Dispose()
    {
        _dataBaseContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<int> Save() => await _dataBaseContext.SaveChangesAsync();
}