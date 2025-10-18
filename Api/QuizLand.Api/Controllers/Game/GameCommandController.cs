using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Commands.Game;
using QuizLand.Application.Contract.Commands.RoundQuestionAnswers;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Api.Controllers.Game;

[Authorize]
public class GameCommandController : BaseCommandController
{
    public GameCommandController(ICommandBus bus) : base(bus)
    {
    }

    [HttpPost("StartTwoPlayerGames")]
    public async Task<ActionResult<CommandResult>> SendNotification([FromBody]StartTwoPlayerGameCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    
    [HttpPost("AnswerRoundQuestions")]
    public async Task<ActionResult<CommandResult>> AnswerRoundQuestion([FromBody]SubmitRoundQuestionAnswersCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
}