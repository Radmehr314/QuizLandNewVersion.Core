using QuizLand.Domain.Models.Avatars;
using QuizLand.Domain.Models.CodeLogs;
using QuizLand.Domain.Models.Courses;
using QuizLand.Domain.Models.ErrorLogs;
using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;
using QuizLand.Domain.Models.Questions;
using QuizLand.Domain.Models.RandQuestionAnswers;
using QuizLand.Domain.Models.RandQuestions;
using QuizLand.Domain.Models.Rands;
using QuizLand.Domain.Models.Supporters;
using QuizLand.Domain.Models.TicketMessages;
using QuizLand.Domain.Models.Tickets;
using QuizLand.Domain.Models.Users;

namespace QuizLand.Domain;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; set; }
    ICodeLogsRepository CodeLogsRepository { get; set; }
    ISupporterRepository SupporterRepository { get; set; }
    ITicketRepository TicketRepository { get; set; }
    ITicketMessageRepository TicketMessageRepository { get; set; }
    ICourseRepository CourseRepository { get; set; }
    IQuestionRepository QuestionRepository { get; set; }
    IGameRepository GameRepository { get; set; }
    IGamerRepository GamerRepository { get; set; }
    IAvatarRepository AvatarRepository { get; set; }
    IRoundQuestionRepository RoundQuestionRepository { get; set; }
    IRoundRepository RoundRepository { get; set; }
    IRoundQuestionAnswerRepository RoundQuestionAnswerRepository { get; set; }
    IErrorLogRepository ErrorLogRepository { get; set; }
    Task<int> Save();

}