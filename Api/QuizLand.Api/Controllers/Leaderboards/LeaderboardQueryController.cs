using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Leaderboard;
using QuizLand.Application.Contract.QueryResults.Leaderboard;

namespace QuizLand.Api.Controllers.Leaderboards;

[Authorize]
public class LeaderboardQueryController : BaseQueryController
{
    public LeaderboardQueryController(IQueryBus bus) : base(bus)
    {
    }

    [HttpGet("GetWeeklyLeaderboard")]
    public async Task<ActionResult<List<GetWeeklyLeaderboardQueryResult>>> WeeklyLeaderboard(
        [FromQuery] GetWeeklyLeaderboardQuery query)
    {
        return Ok(await Bus.Dispatch<GetWeeklyLeaderboardQuery,List<GetWeeklyLeaderboardQueryResult>>(query));
    }
    
    [HttpGet("GetMonthlyLeaderboard")]
    public async Task<ActionResult<List<GetMonthlyLeaderboardQueryResult>>> WeeklyLeaderboard(
        [FromQuery] GetMonthlyLeaderboardQuery query)
    {
        return Ok(await Bus.Dispatch<GetMonthlyLeaderboardQuery,List<GetMonthlyLeaderboardQueryResult>>(query));
    }
}