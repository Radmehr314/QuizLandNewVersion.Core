using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Commands.Course;
using QuizLand.Application.Contract.Commands.Question;
using QuizLand.Application.Contract.Commands.Ticket;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.Questions;

public class QuestionCommandController : BaseCommandController
{

    public QuestionCommandController(ICommandBus bus) : base(bus)
    {
    }
    
    [HttpPost("AddNewQuestion")]
    public async Task<ActionResult<CommandResult>> AddNewCourse([FromBody]AddQuestionCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    [HttpPut("UpdateQuestion")]
    public async Task<ActionResult<CommandResult>> UpdateNewCourse([FromBody]UpdateQuestionCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    [HttpDelete("DeleteQuestion")]
    public async Task<ActionResult<CommandResult>> DeleteNewCourse([FromBody]DeleteQuestionCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    
    [HttpPost("ImportQuestionsFromExcel")]
    public async Task<ActionResult<CommandResult>> ImportQuestionsFromExcel([FromForm]ImportQuestionsFromExcelCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    


   
}