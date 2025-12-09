using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.Notifications;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public NotificationRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task AddNewNotification(Notification notification) => await _dataBaseContext.AddAsync(notification);

    public async Task<List<Notification>> GetAllById(List<long> ids) =>
        await _dataBaseContext.Notifications.Where(f => ids.Contains(f.Id)).ToListAsync();

    public async Task<List<Notification>> AllMyNotification(Guid userId) =>
        await _dataBaseContext.Notifications.Where(f => f.UserId == userId && f.IsSeen == false).ToListAsync();
}