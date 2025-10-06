using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Commands.TicketMessage;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.TicketMessage;

[Authorize]
public class TicketMessageCommandController : BaseCommandController
{
    public TicketMessageCommandController(ICommandBus bus) : base(bus)
    {
    }
    [HttpPost("SubmitMessage")]
    public async Task<ActionResult<CommandResult>> AddUser([FromBody] SubmitNewMessageInTicketCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
}