using QuizLand.Application.Contract.Commands.FriendRequest;
using QuizLand.Application.Contract.Commands.GameRequest;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.QueryResults.Notification;
using QuizLand.Domain.Models.Notifications;

namespace QuizLand.Application.Mapper;

public static class NotificationMapper
{
    public static Notification NewNotificationForRequestFriend(this SendNewFriendRequestCommand command,Guid userId,string username)
    {
        return new Notification()
        {
            Title = $"درخواست دوستی جدید از {username}",
            Message = $"کاربر {username} برای شما یک درخواست دوستی ارسال کرده است.",
            UserId = userId,
            CreatedAt = DateTime.Now,
            IsSeen = false,
            NotificationTypes = NotificationTypes.FriendRequest
        };
    }
    
    public static Notification NewNotificationForAcceptRequestFriend(this AcceptFriendRequestCommand command,Guid userId,string username)
    {
        return new Notification()
        {
            Title = $"پذیرفت درخواست {username}",
            Message = $"کاربر {username} درخواست دوستی شما را پذیرفت.",
            UserId = userId,
            CreatedAt = DateTime.Now,
            IsSeen = false,
            NotificationTypes = NotificationTypes.AcceptFriendRequest
        };
    }
    
    public static Notification NewNotificationForRejectRequestFriend(this RejectFriendRequestCommand command,Guid userId,string username)
    {
        return new Notification()
        {
            Title = $"رد درخواست {username}",
            Message = $"کاربر {username} درخواست دوستی شما را رد کرد.",
            UserId = userId,
            CreatedAt = DateTime.Now,
            IsSeen = false,
            NotificationTypes = NotificationTypes.RejectAcceptFriendRequest
        };
    }

    public static List<AllMyNotificationsQueryResult> AllMyNotificationMapper(this List<Notification> notifications)
    {
        return notifications.Select(f => new AllMyNotificationsQueryResult()
        {
            Id = f.Id,
            Message = f.Message,
            Title = f.Title,
            NotificationTypes = f.NotificationTypes,
            CreatedAt = f.CreatedAt.ToShamsiDate()
        }).ToList();
    }
    
    public static Notification NewNotificationForGameRequest(this SendNewGameRequestCommand command,Guid userId,string username)
    {
        return new Notification()
        {
            Title = $"درخواست بازی جدید از {username}",
            Message = $"کاربر {username} برای شما یک درخواست بازی ارسال کرده است.",
            UserId = userId,
            CreatedAt = DateTime.Now,
            IsSeen = false,
            NotificationTypes = NotificationTypes.GameRequest
        };
    }
    
    public static Notification NewNotificationForAcceptGameRequest(this AcceptGameRequestCommand command,Guid userId,string username)
    {
        return new Notification()
        {
            Title = $"پذیرفت درخواست {username}",
            Message = $"کاربر {username} درخواست بازی شما را پذیرفت.",
            UserId = userId,
            CreatedAt = DateTime.Now,
            IsSeen = false,
            NotificationTypes = NotificationTypes.AcceptFriendRequest
        };
    }
    
    public static Notification NewNotificationForRejectGameRequest(this RejectGameRequestCommand command,Guid userId,string username)
    {
        return new Notification()
        {
            Title = $"رد درخواست {username}",
            Message = $"کاربر {username} درخواست بازی شما را رد کرد.",
            UserId = userId,
            CreatedAt = DateTime.Now,
            IsSeen = false,
            NotificationTypes = NotificationTypes.RejectAcceptFriendRequest
        };
    }


    
}