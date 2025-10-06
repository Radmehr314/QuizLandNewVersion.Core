using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.User;

public class UpdateUserCommand : ICommand
{
    public Guid Id { get; set; }
    //ToDo Fix Later
}