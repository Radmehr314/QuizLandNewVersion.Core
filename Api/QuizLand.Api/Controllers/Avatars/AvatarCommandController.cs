using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Commands.Avatar;
using QuizLand.Application.Contract.Commands.Course;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.Avatars;

public class AvatarCommandController : BaseCommandController
{
    public AvatarCommandController(ICommandBus bus) : base(bus)
    {
    }
    
        
    [HttpPost("AddAvatar")]
    public async Task<ActionResult<CommandResult>> AddNewCourse([FromForm]AddAvatarCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }

    [HttpDelete("DeleteAvatar")]
    public async Task<ActionResult<CommandResult>> DeleteNewCourse([FromBody]DeleteAvatarCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    
}