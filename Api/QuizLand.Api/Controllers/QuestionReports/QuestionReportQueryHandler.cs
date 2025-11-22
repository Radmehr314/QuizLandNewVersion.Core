using Microsoft.AspNetCore.Mvc;
using QuizLand.Api.Framework;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.QuestionReport;
using QuizLand.Application.Contract.QueryResults.QuestionReport;

namespace QuizLand.Api.Controllers.QuestionReports;

public class QuestionReportQueryHandler :  BaseQueryController
{
    public QuestionReportQueryHandler(IQueryBus bus) : base(bus)
    {
    }

    [HttpGet("GetAllQuestionReport")]
    public async Task<ActionResult<List<GetAllQuestionReportQueryResult>>> GetAllQuestionReport(
        [FromQuery] GetAllQuestionReportQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllQuestionReportQuery,List<GetAllQuestionReportQueryResult>>(query));
    }
}