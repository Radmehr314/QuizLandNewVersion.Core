using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.FriendRequest;

public class SendNewFriendRequestCommand : ICommand
{
    public Guid Id { get; set; }
}