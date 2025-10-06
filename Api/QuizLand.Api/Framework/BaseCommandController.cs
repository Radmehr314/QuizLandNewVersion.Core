
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Framework;

public class BaseCommandController:BaseController
{
    protected readonly ICommandBus Bus;
    public BaseCommandController(ICommandBus bus)
    {
        Bus = bus;
    }
}