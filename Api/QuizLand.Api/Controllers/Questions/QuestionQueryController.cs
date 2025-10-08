using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Course;
using QuizLand.Application.Contract.Queries.Question;
using QuizLand.Application.Contract.Queries.Ticket;
using QuizLand.Application.Contract.QueryResults.Course;
using QuizLand.Application.Contract.QueryResults.Question;
using QuizLand.Application.Contract.QueryResults.Ticket;

namespace QuizLand.Api.Controllers.Questions;

public class QuestionQueryController : BaseQueryController
{
    public QuestionQueryController(IQueryBus bus) : base(bus)
    {
    }
    
    [HttpGet("AllPagiantion")]
    public async Task<ActionResult<GetAllQuestionPaginationQueryResult>> AllCoursesPagination([FromQuery]GetAllQuestionPaginationQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllQuestionPaginationQuery,GetAllQuestionPaginationQueryResult>(query));
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult<GetByIdQuestionQueryResult>> GetCourse([FromQuery]GetByIdQuestionQuery query)
    {
        return Ok(await Bus.Dispatch<GetByIdQuestionQuery,GetByIdQuestionQueryResult>(query));
    }
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<GetAllQuestionQueryResult>>> AllCourses([FromQuery]GetAllQuestionQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllQuestionQuery,List<GetAllQuestionQueryResult>>(query));
    }
}