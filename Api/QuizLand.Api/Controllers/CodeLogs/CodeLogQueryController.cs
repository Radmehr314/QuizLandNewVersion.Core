using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.CodeLog;
using QuizLand.Application.Contract.Queries.User;
using QuizLand.Application.Contract.QueryResults.CodeLog;
using QuizLand.Application.Contract.QueryResults.User;

namespace QuizLand.Api.Controllers.CodeLogs;

public class CodeLogQueryController :  BaseQueryController
{
    public CodeLogQueryController(IQueryBus bus) : base(bus)
    {
    }
    
    
    [HttpGet("SendCode")]
    public async Task<ActionResult<GetCodeQueryResult>> GetUser([FromQuery]GetCodeQuery query)
    {
        return Ok(await Bus.Dispatch<GetCodeQuery,GetCodeQueryResult>(query));
    }
    

    [HttpGet("GetAllPagination")]
    public async Task<ActionResult<GetCodeQueryResult>> All([FromQuery]GetAllCodeLogPaginationQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllCodeLogPaginationQuery,GetAllCodeLogPaginationQueryResult>(query));
    }
    
    [HttpGet("GetAllByPersonalCodePagination")]
    public async Task<ActionResult<GetCodeQueryResult>> GetByPersonelCode([FromQuery]GetAllCodeLogByPersonalCodePaginationQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllCodeLogByPersonalCodePaginationQuery,GetAllCodeLogByPersonalCodePaginationQueryResult>(query));
    }
}