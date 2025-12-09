using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Notification;

public class SeenNotificationsCommand : ICommand
{
    public List<long> Ids { get; set; }
}