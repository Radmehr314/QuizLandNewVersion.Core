using System;
using System.Threading.Tasks;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Notification;
using QuizLand.Domain;

namespace QuizLand.Application.CommandHandler;

public class NotificationCommandHandler : ICommandHandler<SeenNotificationsCommand>
{
    private readonly IUnitOfWork  _unitOfWork;
    private readonly IRealTimeNotifier _realTimeNotifier;

    public NotificationCommandHandler(IUnitOfWork unitOfWork, IRealTimeNotifier realTimeNotifier)
    {
        _unitOfWork = unitOfWork;
        _realTimeNotifier = realTimeNotifier;
    }


    public async Task<CommandResult> Handle(SeenNotificationsCommand command)
    {
        var notification = await _unitOfWork.NotificationRepository.GetAllById(command.Ids);
        foreach (var item in notification)
        {
            item.IsSeen = true;
        }

        await _unitOfWork.Save();
        return new CommandResult();
    }
}