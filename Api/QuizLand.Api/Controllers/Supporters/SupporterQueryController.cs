using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Login;
using QuizLand.Application.Contract.Queries.Supporter;
using QuizLand.Application.Contract.Queries.User;
using QuizLand.Application.Contract.QueryResults.Login;
using QuizLand.Application.Contract.QueryResults.Suporter;
using QuizLand.Application.Contract.QueryResults.User;

namespace QuizLand.Api.Controllers.Supporters;

[Authorize(Roles = "Supporter")]
public class SupporterQueryController : BaseQueryController
{
    public SupporterQueryController(IQueryBus bus) : base(bus)
    {
    }
    
    [HttpGet("All")]
    public async Task<ActionResult<List<GetAllSupporterQueryResult>>> AllUser([FromQuery]GetAllSupporterQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllSupporterQuery,List<GetAllSupporterQueryResult>>(query));
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult<GetSupporterByIdQueryResult>> GetUser([FromQuery]GetSupporterByIdQuery query)
    {
        return Ok(await Bus.Dispatch<GetSupporterByIdQuery,GetSupporterByIdQueryResult>(query));
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginDto>> Login([FromBody] LoginSupporterRequestDto query)
    {
        return Ok(await Bus.Dispatch<LoginSupporterRequestDto,LoginDto>(query));

    }
}