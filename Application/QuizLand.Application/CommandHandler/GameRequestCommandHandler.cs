using QuizLand.Application.Contract.Commands.GameRequest;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Mapper;
using QuizLand.Domain;
using QuizLand.Domain.Models.GameRequests;

namespace QuizLand.Application.CommandHandler;

public class GameRequestCommandHandler : ICommandHandler<SendNewGameRequestCommand> , ICommandHandler<AcceptGameRequestCommand>,ICommandHandler<RejectGameRequestCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IRealTimeNotifier _realTimeNotifier;

    public GameRequestCommandHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService, IRealTimeNotifier realTimeNotifier)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _realTimeNotifier = realTimeNotifier;
    }
    public async Task<CommandResult> Handle(SendNewGameRequestCommand command)
    {
        var exist = await _unitOfWork.GameRequestRepository.Exist(_userInfoService.GetUserIdByToken(), command.userId);
        if (exist) throw new UserAccessException("درخواست قبلی هنوز تایید یا رد نشده است.");
        var user = await _unitOfWork.UserRepository.GetById(_userInfoService.GetUserIdByToken());
        var gameRequest = command.Factory(_userInfoService.GetUserIdByToken());
        var notification = command.NewNotificationForGameRequest(command.userId,user.Username);
        await _unitOfWork.NotificationRepository.AddNewNotification(notification);
        await _realTimeNotifier.SendToUserAsync(command.userId.ToString(),"NewNotification",new {Notification = notification,at = DateTime.UtcNow});
        await _unitOfWork.Save();
        return new CommandResult() { Id = gameRequest.Id };
    }

    public async Task<CommandResult> Handle(AcceptGameRequestCommand command)
    {
        var request = await _unitOfWork.GameRequestRepository.GetById(command.RequestId);
        if (request is null)
        {
            throw new NotFoundException("شناسه درخواست نامعتبر است");
        }
        request.RespondedAt =  DateTime.Now;
        request.GameRequestStatus = GameRequestStatus.Accepted;
        var user = await _unitOfWork.UserRepository.GetById(request.SecondUserId);
        var notification = command.NewNotificationForAcceptGameRequest(request.FirstUserId,user.Username);
        await _unitOfWork.NotificationRepository.AddNewNotification(notification);
        await _realTimeNotifier.SendToUserAsync(request.FirstUserId.ToString(),"NewNotification",new {Notification = notification,at = DateTime.UtcNow});
        await _unitOfWork.Save();
        return new CommandResult() { Id = request.Id };
    }

    public async Task<CommandResult> Handle(RejectGameRequestCommand command)
    {
        var request = await _unitOfWork.GameRequestRepository.GetById(command.RequestId);
        request.RespondedAt =  DateTime.Now;
        request.GameRequestStatus = GameRequestStatus.Rejected;
        var user = await _unitOfWork.UserRepository.GetById(request.SecondUserId);
        var notification = command.NewNotificationForRejectGameRequest(request.FirstUserId,user.Username);
        await _unitOfWork.NotificationRepository.AddNewNotification(notification);
        await _realTimeNotifier.SendToUserAsync(request.FirstUserId.ToString(),"NewNotification",new {Notification = notification,at = DateTime.UtcNow});
        await _unitOfWork.Save();
        return new CommandResult() { Id = request.Id };
    }
}