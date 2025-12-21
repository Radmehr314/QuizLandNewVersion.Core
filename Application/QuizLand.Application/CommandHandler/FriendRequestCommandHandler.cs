using QuizLand.Application.Contract.Commands.FriendRequest;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Mapper;
using QuizLand.Domain;
using QuizLand.Domain.Models.FriendRequests;

namespace QuizLand.Application.CommandHandler;

public class FriendRequestCommandHandler : ICommandHandler<SendNewFriendRequestCommand>,ICommandHandler<AcceptFriendRequestCommand>,ICommandHandler<RejectFriendRequestCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IRealTimeNotifier _realTimeNotifier;


    public FriendRequestCommandHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService, IRealTimeNotifier realTimeNotifier)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _realTimeNotifier = realTimeNotifier;
    }
    public async Task<CommandResult> Handle(SendNewFriendRequestCommand command)
    {
        var userId = _userInfoService.GetUserIdByToken();
        if(userId == command.Id) throw new UserAccessException("نمیتوانید به خودتان درخواست دوستی بدین.");
        
        var user = await _unitOfWork.UserRepository.GetById(command.Id);
        var sender = await _unitOfWork.UserRepository.GetById(_userInfoService.GetUserIdByToken());
        if (user is null)
        {
            throw new NotFoundException("کاربر یافت نشد");
        }

        var exist = await _unitOfWork.FriendRequestRepository.Exist(sender.Id, user.Id);
        if (exist) throw new UserAccessException("درخواست دوستی قبلی هنوز تایید یا رد نشده است.");
        var data = command.Factory(_userInfoService.GetUserIdByToken(),user.Id);
        await _unitOfWork.FriendRequestRepository.SendNewRequest(data);
        var notification = command.NewNotificationForRequestFriend(user.Id,sender.Username);
        await _unitOfWork.NotificationRepository.AddNewNotification(notification);
        await _realTimeNotifier.SendToUserAsync(command.Id.ToString(),"NewNotification",new {Notification = "یه پیام تازه برات اومده 🙂",at = DateTime.UtcNow});
        await _unitOfWork.Save();
        return new CommandResult(){Id = data.Id};
    }

    public async Task<CommandResult> Handle(AcceptFriendRequestCommand command)
    {
        
        var request = await _unitOfWork.FriendRequestRepository.GetById(command.RequestId);
        if (request is null)
        {
            throw new NotFoundException("شناسه درخواست نامعتبر است");
        }
        request.RespondedAt =  DateTime.Now;
        request.FriendRequestStatus = FriendRequestStatus.Accepted;
        var friend = request.Factory();
        await _unitOfWork.FriendRepository.Add(friend);
        var user = await _unitOfWork.UserRepository.GetById(request.ReceiverId);
        var notification = command.NewNotificationForAcceptRequestFriend(request.RequesterId,user.Username);
        await _unitOfWork.NotificationRepository.AddNewNotification(notification);
        await _realTimeNotifier.SendToUserAsync(request.RequesterId.ToString(),"NewNotification",new{Notification = "یه پیام تازه برات اومده 🙂",at = DateTime.UtcNow});
        await _unitOfWork.Save();
        return new CommandResult() { Id = request.Id };
    }

    public async Task<CommandResult> Handle(RejectFriendRequestCommand command)
    {
        var request = await _unitOfWork.FriendRequestRepository.GetById(command.RequestId);
        request.RespondedAt =  DateTime.Now;
        request.FriendRequestStatus = FriendRequestStatus.Rejected;
        var user = await _unitOfWork.UserRepository.GetById(request.ReceiverId);
        var notification = command.NewNotificationForRejectRequestFriend(request.RequesterId,user.Username);
        await _unitOfWork.NotificationRepository.AddNewNotification(notification);
        await _realTimeNotifier.SendToUserAsync(request.RequesterId.ToString(),"NewNotification",new {Notification = "یه پیام تازه برات اومده 🙂",at = DateTime.UtcNow});
        await _unitOfWork.Save();
        return new CommandResult() { Id = request.Id };
    }
}