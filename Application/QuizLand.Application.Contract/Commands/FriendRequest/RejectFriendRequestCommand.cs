using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.FriendRequest;

public class RejectFriendRequestCommand : ICommand
{
    public long RequestId { get; set; }
}