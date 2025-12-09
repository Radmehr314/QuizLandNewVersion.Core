using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.FriendRequest;
using QuizLand.Application.Contract.QueryResults.FriendRequest;

namespace QuizLand.Api.Controllers.FriendRequest;

public class FriendRequestQueryController :  BaseQueryController
{
    public FriendRequestQueryController(IQueryBus bus) : base(bus)
    {
    }

    [HttpGet("AllMyFriendRequests")]
    public async Task<ActionResult<List<AllMyFriendRequestsQueryResult>>> AllMyFriendRequest(
        [FromQuery] AllMyFriendRequestsQuery query)
    {
        return Ok(await Bus.Dispatch<AllMyFriendRequestsQuery,List<AllMyFriendRequestsQueryResult>>(query));
    }
}