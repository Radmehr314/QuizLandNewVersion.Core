using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Game;
using QuizLand.Application.Contract.QueryResults.Game;

namespace QuizLand.Api.Controllers.Game;

[Authorize]
public class GameQueryController : BaseQueryController
{
    public GameQueryController(IQueryBus bus) : base(bus)
    {
    }
    
    [HttpGet("AllMyRunningGames")]
    public async Task<ActionResult<List<GetAllMyRunningGamesQueryResult>>> AllUser([FromQuery]GetAllMyRunningGamesQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllMyRunningGamesQuery,List<GetAllMyRunningGamesQueryResult>>(query));
    }
    
    [HttpGet("GetGameById")]
    public async Task<ActionResult<GetGameByIdQueryResult>> AllUser([FromQuery]GetGameByIdQuery query)
    {
        return Ok(await Bus.Dispatch<GetGameByIdQuery,GetGameByIdQueryResult>(query));
    }
}