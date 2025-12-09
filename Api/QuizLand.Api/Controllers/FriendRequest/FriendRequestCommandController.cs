using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Commands.FriendRequest;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.FriendRequest;

[Authorize]
public class FriendRequestCommandController : BaseCommandController
{
    public FriendRequestCommandController(ICommandBus bus) : base(bus)
    {
    }

    [HttpPost("SendNewRequest")]
    public async Task<ActionResult<CommandResult>> SendNewRequest([FromBody] SendNewFriendRequestCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    
    [HttpPost("AcceptFriendRequest")]
    public async Task<ActionResult<CommandResult>> AcceptFriendRequest([FromBody] AcceptFriendRequestCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    
    [HttpPost("RejectFriendRequest")]
    public async Task<ActionResult<CommandResult>> SendNewRequest([FromBody] RejectFriendRequestCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
}