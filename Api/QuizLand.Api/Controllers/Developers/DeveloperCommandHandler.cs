using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Commands.Developer;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.Developers;

public class DeveloperCommandHandler : BaseCommandController
{
    public DeveloperCommandHandler(ICommandBus bus) : base(bus)
    {
    }

    [HttpDelete("DeleteAllGame")]
    public async Task<ActionResult<CommandResult>> DeleteAllGames([FromQuery] DeleteAllGamesCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
}