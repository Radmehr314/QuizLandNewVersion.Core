namespace QuizLand.Domain.Models.Notifications;

public interface INotificationRepository
{
    Task AddNewNotification(Notification notification);
    Task<List<Notification>> GetAllById(List<long> ids);
    Task<List<Notification>> AllMyNotification(Guid userId);
}