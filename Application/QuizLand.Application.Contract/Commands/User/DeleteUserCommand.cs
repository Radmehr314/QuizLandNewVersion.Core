using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.User;

public class DeleteUserCommand : ICommand
{
    public Guid Id { get; set; }
}