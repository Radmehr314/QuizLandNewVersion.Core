using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Round;
using QuizLand.Application.Contract.Queries.Supporter;
using QuizLand.Application.Contract.QueryResults.Round;
using QuizLand.Application.Contract.QueryResults.Suporter;

namespace QuizLand.Api.Controllers.Rounds;

[Authorize]
public class RoundQueryController : BaseQueryController
{
    public RoundQueryController(IQueryBus bus) : base(bus)
    {
    }
    
    [HttpGet("GetRoundQuestion")]
    public async Task<ActionResult<GetRoundQuestionsQueryResult>> AllUser([FromQuery]GetRoundQuestionsQuery query)
    {
        return Ok(await Bus.Dispatch<GetRoundQuestionsQuery,GetRoundQuestionsQueryResult>(query));
    }
}