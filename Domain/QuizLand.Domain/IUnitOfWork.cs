using QuizLand.Domain.Leaderboard;
using QuizLand.Domain.Models.Avatars;
using QuizLand.Domain.Models.CodeLogs;
using QuizLand.Domain.Models.Courses;
using QuizLand.Domain.Models.ErrorLogs;
using QuizLand.Domain.Models.FriendRequests;
using QuizLand.Domain.Models.Friends;
using QuizLand.Domain.Models.GameRequests;
using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;
using QuizLand.Domain.Models.Notifications;
using QuizLand.Domain.Models.Provinces;
using QuizLand.Domain.Models.QuestionReports;
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
    IProvinceRepository ProvinceRepository { get; set; }
    IQuestionReportRepository QuestionReportRepository { get; set; }
    IFriendRequestRepository FriendRequestRepository { get; set; }
    IFriendRepository FriendRepository { get; set; }
    INotificationRepository NotificationRepository { get; set; }
    IGameRequestRepository GameRequestRepository { get; set; }
    ILeaderboardRepository LeaderboardRepository { get; set; }
    Task<int> Save();

}