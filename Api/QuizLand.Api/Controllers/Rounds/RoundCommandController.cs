using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.Rounds;

public class RoundCommandController : BaseCommandController
{
    public RoundCommandController(ICommandBus bus) : base(bus)
    {
    }
}