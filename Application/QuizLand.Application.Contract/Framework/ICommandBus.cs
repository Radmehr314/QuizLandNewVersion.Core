namespace QuizLand.Application.Contract.Framework;

public interface ICommandBus
{
    Task<CommandResult> Dispatch<T>(T command) where T : ICommand;
}