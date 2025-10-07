using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Course;
using QuizLand.Application.Contract.Queries.Ticket;
using QuizLand.Application.Contract.QueryResults.Course;
using QuizLand.Application.Contract.QueryResults.Ticket;

namespace QuizLand.Api.Controllers.Courses;

public class CourseQueryController : BaseQueryController
{
    public CourseQueryController(IQueryBus bus) : base(bus)
    {
    }
    
    [HttpGet("AllPagiantion")]
    public async Task<ActionResult<GetAllCoursePaginationQueryResult>> AllCoursesPagination([FromQuery]GetAllCoursePaginationQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllCoursePaginationQuery,GetAllCoursePaginationQueryResult>(query));
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult<GetCourseByIdQueryResult>> GetCourse([FromQuery]GetCourseByIdQuery query)
    {
        return Ok(await Bus.Dispatch<GetCourseByIdQuery,GetCourseByIdQueryResult>(query));
    }
    [HttpGet("GetAll")]
    public async Task<ActionResult<GetAllCourseQueryResult>> AllCourses([FromQuery]GetAllCourseQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllCourseQuery,GetAllCourseQueryResult>(query));
    }
}