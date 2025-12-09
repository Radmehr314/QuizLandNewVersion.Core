using QuizLand.Domain.Models.Notifications;

namespace QuizLand.Application.Contract.QueryResults.Notification;

public class AllMyNotificationsQueryResult
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public NotificationTypes  NotificationTypes { get; set; }
    public string CreatedAt { get; set; }
}