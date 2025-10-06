using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Commands.Supporter;
using QuizLand.Application.Contract.Commands.User;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.Supporters;

public class SupporterCommandController : BaseCommandController
{
    public SupporterCommandController(ICommandBus bus) : base(bus)
    {
    }
    
    [HttpPost("AddSupporter")]
    public async Task<ActionResult<CommandResult>> AddUser([FromBody]AddSupporterCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    
    [HttpPut("SupporterStatus")]
    public async Task<ActionResult<CommandResult>> UserStatus([FromBody] SupporterStatusCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }

}