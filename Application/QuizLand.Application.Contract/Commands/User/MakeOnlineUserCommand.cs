using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.User;

public class MakeOnlineUserCommand : ICommand
{
    public Guid Id { get; set; }
}