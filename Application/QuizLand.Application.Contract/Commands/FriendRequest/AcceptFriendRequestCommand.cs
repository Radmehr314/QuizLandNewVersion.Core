using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.FriendRequest;

public class AcceptFriendRequestCommand : ICommand
{
    public long RequestId { get; set; }
}