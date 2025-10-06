
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Framework;

public class BaseQueryController:BaseController
{
    protected readonly IQueryBus Bus;

    public BaseQueryController(IQueryBus bus)
    {
        Bus = bus;
    }
}