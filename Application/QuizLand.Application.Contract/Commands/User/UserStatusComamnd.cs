using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.User;

public class UserStatusComamnd : ICommand
{
    public Guid Id { get; set; }
}