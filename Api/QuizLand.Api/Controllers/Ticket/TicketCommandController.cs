using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Commands.Supporter;
using QuizLand.Application.Contract.Commands.Ticket;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.Ticket;

[Authorize]
public class TicketCommandController : BaseCommandController
{
    public TicketCommandController(ICommandBus bus) : base(bus)
    {
    }
    [HttpPost("SubmitNewTicket")]
    public async Task<ActionResult<CommandResult>> AddUser([FromBody]SubmitNewTicketCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }

}