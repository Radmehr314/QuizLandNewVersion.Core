using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Commands.GameRequest;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.GameRequest;

public class GameRequestCommandController :  BaseCommandController
{
    public GameRequestCommandController(ICommandBus bus) : base(bus)
    {
    }

    [HttpPost("SendGameRequest")]
    public async Task<ActionResult<CommandResult>> SendNewGameRequest([FromBody] SendNewGameRequestCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    } 
    
    [HttpPost("AcceptGameRequest")]
    public async Task<ActionResult<CommandResult>> AcceptFriendRequest([FromBody] AcceptGameRequestCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    [HttpPost("RejectFriendRequest")]
    public async Task<ActionResult<CommandResult>> AcceptFriendRequest([FromBody] RejectGameRequestCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
}