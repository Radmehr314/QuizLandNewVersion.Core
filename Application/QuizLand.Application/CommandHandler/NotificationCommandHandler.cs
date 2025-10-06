using System;
using System.Threading.Tasks;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Notification;
using QuizLand.Domain;

namespace QuizLand.Application.CommandHandler;

public class NotificationCommandHandler : ICommandHandler<SendNotificationCommand>
{
    private readonly IUnitOfWork  _unitOfWork;
    private readonly IRealTimeNotifier _realTimeNotifier;

    public NotificationCommandHandler(IUnitOfWork unitOfWork, IRealTimeNotifier realTimeNotifier)
    {
        _unitOfWork = unitOfWork;
        _realTimeNotifier = realTimeNotifier;
    }
    public async Task<CommandResult> Handle(SendNotificationCommand command)
    {
        var user  = await _unitOfWork.UserRepository.GetById(command.UserId);
        await _realTimeNotifier.SendToUserAsync(user.Id.ToString(),"UserNotification",new {text = command.Content, at = DateTime.UtcNow});
        return new CommandResult() { Id = user.Id };
    }

    
}