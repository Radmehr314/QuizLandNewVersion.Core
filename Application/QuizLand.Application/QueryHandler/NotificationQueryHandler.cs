using System;
using System.Threading.Tasks;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Notification;
using QuizLand.Application.Contract.QueryResults.Notification;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class NotificationQueryHandler  :IQueryHandler<AllMyNotificationsQuery,List<AllMyNotificationsQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRealTimeNotifier _realTimeNotifier;
    private readonly IUserInfoService _userInfoService;


    public NotificationQueryHandler(IUnitOfWork unitOfWork, IRealTimeNotifier realTimeNotifier, IUserInfoService userInfoService)
    {
        _unitOfWork = unitOfWork;
        _realTimeNotifier = realTimeNotifier;
        _userInfoService = userInfoService;
    }

    public async Task<List<AllMyNotificationsQueryResult>> Handle(AllMyNotificationsQuery query)
    {
        var notification =
            await _unitOfWork.NotificationRepository.AllMyNotification(_userInfoService.GetUserIdByToken());
        return notification.AllMyNotificationMapper();
    }
}