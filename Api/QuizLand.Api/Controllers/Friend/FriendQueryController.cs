using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Friend;
using QuizLand.Application.Contract.QueryResults.Friend;

namespace QuizLand.Api.Controllers.Friend;

public class FriendQueryController : BaseQueryController
{
    public FriendQueryController(IQueryBus bus) : base(bus)
    {
    }

    [HttpGet("AllMyFriends")]
    public async Task<ActionResult<List<AllMyFriendQueryResult>>> AllMyFriends([FromQuery] AllMyFriendQuery query)
    {
        return Ok(await Bus.Dispatch<AllMyFriendQuery,List<AllMyFriendQueryResult>>(query));
    }
}