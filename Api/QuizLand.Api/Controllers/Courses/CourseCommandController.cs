using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Commands.Course;
using QuizLand.Application.Contract.Commands.Ticket;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.Courses;

public class CourseCommandController : BaseCommandController
{
    public CourseCommandController(ICommandBus bus) : base(bus)
    {
    }
    
    [HttpPost("AddNewCourse")]
    public async Task<ActionResult<CommandResult>> AddNewCourse([FromBody]AddCourseCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    [HttpPut("UpdateNewCourse")]
    public async Task<ActionResult<CommandResult>> UpdateNewCourse([FromBody]UpdateCourseCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    [HttpDelete("DeleteCourse")]
    public async Task<ActionResult<CommandResult>> DeleteNewCourse([FromBody]DeleteCourseCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
}