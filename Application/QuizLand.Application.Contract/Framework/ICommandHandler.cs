namespace QuizLand.Application.Contract.Framework;

public interface ICommandHandler<in T> where T : ICommand
{
    Task<CommandResult> Handle(T command);
}