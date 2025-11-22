using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Commands.QuestionReport;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.QuestionReports;

[Authorize]
public class QuestionReportCommandHandler :  BaseCommandController
{
    public QuestionReportCommandHandler(ICommandBus bus) : base(bus)
    {
    }

    [HttpPost("AddQuestionReport")]
    public async Task<ActionResult<CommandResult>> AddQuestionReport([FromBody] AddQuestionReportCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
}