using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Notification;

public class SendNotificationCommand : ICommand
{
    public Guid UserId { get; set; }
    public string Content { get; set; }
}