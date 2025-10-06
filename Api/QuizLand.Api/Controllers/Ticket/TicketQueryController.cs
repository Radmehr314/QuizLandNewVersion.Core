using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Supporter;
using QuizLand.Application.Contract.Queries.Ticket;
using QuizLand.Application.Contract.QueryResults.Suporter;
using QuizLand.Application.Contract.QueryResults.Ticket;

namespace QuizLand.Api.Controllers.Ticket;

public class TicketQueryController : BaseQueryController
{
    public TicketQueryController(IQueryBus bus) : base(bus)
    {
    }
    
    [HttpGet("AllPagiantion")]
    public async Task<ActionResult<GetAllTicketPaginationQueryResult>> AllUser([FromQuery]GetAllTicketPaginationQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllTicketPaginationQuery,GetAllTicketPaginationQueryResult>(query));
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult<GetTicketByIdQueryResult>> GetUser([FromQuery]GetTicketByIdQuery query)
    {
        return Ok(await Bus.Dispatch<GetTicketByIdQuery,GetTicketByIdQueryResult>(query));
    }
}