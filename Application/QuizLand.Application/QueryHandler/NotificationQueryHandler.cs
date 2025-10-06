using System;
using System.Threading.Tasks;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Notification;

using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class NotificationQueryHandler 
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRealTimeNotifier _realTimeNotifier;


    public NotificationQueryHandler(IUnitOfWork unitOfWork, IRealTimeNotifier realTimeNotifier)
    {
        _unitOfWork = unitOfWork;
        _realTimeNotifier = realTimeNotifier;
    }
  
}