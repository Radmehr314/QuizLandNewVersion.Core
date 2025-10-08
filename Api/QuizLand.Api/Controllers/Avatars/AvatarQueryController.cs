using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Avatar;
using QuizLand.Application.Contract.QueryResults.Avatar;

namespace QuizLand.Api.Controllers.Avatars;

public class AvatarQueryController : BaseQueryController
{
    public AvatarQueryController(IQueryBus bus) : base(bus)
    {
    }
    
    [HttpGet("GetAllAvatars")]
    public async Task<ActionResult<List<AllAvatarQueryResult>>> All([FromQuery]AllAvatarQuery query)
    {
        return Ok(await Bus.Dispatch<AllAvatarQuery,List<AllAvatarQueryResult>>(query));
    }
    
    [HttpGet("GetAllAvatarPagination")]
    public async Task<ActionResult<AllAvatarPaginationQeuryResult>> All([FromQuery] AllAvatarPaginationQeury query)
    {
        return Ok(await Bus.Dispatch<AllAvatarPaginationQeury,AllAvatarPaginationQeuryResult>(query));
    }
    
    [HttpGet("GetavatarById")]
    public async Task<ActionResult<GetAvatarByIdQueryResult>> All([FromQuery] GetAvatarByIdQuery query)
    {
        return Ok(await Bus.Dispatch<GetAvatarByIdQuery,GetAvatarByIdQueryResult>(query));
    }
}