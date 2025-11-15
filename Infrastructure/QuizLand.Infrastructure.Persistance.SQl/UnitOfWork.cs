using QuizLand.Domain;
using QuizLand.Domain.Models.Avatars;
using QuizLand.Domain.Models.CodeLogs;
using QuizLand.Domain.Models.Courses;
using QuizLand.Domain.Models.ErrorLogs;
using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;
using QuizLand.Domain.Models.Provinces;
using QuizLand.Domain.Models.Questions;
using QuizLand.Domain.Models.RandQuestionAnswers;
using QuizLand.Domain.Models.RandQuestions;
using QuizLand.Domain.Models.Rands;
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
    public ICourseRepository CourseRepository { get; set; }
    public IQuestionRepository QuestionRepository { get; set; }
    public IGameRepository GameRepository { get; set; }
    public IGamerRepository GamerRepository { get; set; }
    public IAvatarRepository AvatarRepository { get; set; }
    public IRoundQuestionRepository RoundQuestionRepository { get; set; }
    public IRoundRepository RoundRepository { get; set; }
    public IRoundQuestionAnswerRepository RoundQuestionAnswerRepository { get; set; }
    public IErrorLogRepository ErrorLogRepository { get; set; }
    public IProvinceRepository ProvinceRepository { get; set; }

    public UnitOfWork(DataBaseContext dataBaseContext, IUserRepository userRepository, ICodeLogsRepository codeLogsRepository, ISupporterRepository supporterRepository, ITicketRepository ticketRepository, ITicketMessageRepository ticketMessageRepository, ICourseRepository courseRepository, IQuestionRepository questionRepository, IGameRepository gameRepository, IGamerRepository gamerRepository, IAvatarRepository avatarRepository, IRoundQuestionRepository roundQuestionRepository, IRoundRepository roundRepository, IRoundQuestionAnswerRepository roundQuestionAnswerRepository, IErrorLogRepository errorLogRepository, IProvinceRepository provinceRepository)
    {
        _dataBaseContext = dataBaseContext;
        UserRepository = userRepository;
        CodeLogsRepository = codeLogsRepository;
        SupporterRepository = supporterRepository;
        TicketRepository = ticketRepository;
        TicketMessageRepository = ticketMessageRepository;
        CourseRepository = courseRepository;
        QuestionRepository = questionRepository;
        GameRepository = gameRepository;
        GamerRepository = gamerRepository;
        AvatarRepository = avatarRepository;
        RoundQuestionRepository = roundQuestionRepository;
        RoundRepository = roundRepository;
        RoundQuestionAnswerRepository = roundQuestionAnswerRepository;
        ErrorLogRepository = errorLogRepository;
        ProvinceRepository = provinceRepository;
    }
    
    public void Dispose()
    {
        _dataBaseContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<int> Save() => await _dataBaseContext.SaveChangesAsync();
}