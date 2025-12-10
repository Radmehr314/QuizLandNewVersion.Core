using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.Game;

public class StartGameWithFriendCommand : ICommand
{
    public Guid UserId { get; set; }
}