using Autofac;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Infrastructure.Config;

public class AutofacCommandBus : ICommandBus
{
    private readonly ILifetimeScope _scope;
    public AutofacCommandBus(ILifetimeScope scope)
    {
        _scope = scope;
    }

    public async Task<CommandResult> Dispatch<T>(T command) where T : ICommand
    {
        var handler = _scope.Resolve<ICommandHandler<T>>();
        return await handler.Handle(command);
    }
}



