using QuizLand.Application.Contract.Commands.Question;
using QuizLand.Application.Contract.Commands.QuestionReport;
using QuizLand.Application.Contract.Queries.QuestionReport;
using QuizLand.Application.Contract.QueryResults.QuestionReport;
using QuizLand.Domain.Models.QuestionReports;

namespace QuizLand.Application.Mapper;

public static class QuestionReportMapper
{
    public static QuestionReport Factory(this AddQuestionReportCommand addQuestionReportCommand,Guid UserId,long questionId)
    {
        return new QuestionReport()
        {
            QuestionId = questionId,
            ReportType = addQuestionReportCommand.ReportType,
            Description = addQuestionReportCommand.Description,
            UserId = UserId
        };
    }
    
    public static List<GetAllQuestionReportQueryResult> GetAllMapper(this List<QuestionReport> questionReports)
    {
        return questionReports.Select(f => new GetAllQuestionReportQueryResult()
        {
            UserId = f.UserId,
            QuestionId = f.QuestionId,
            Username = f.User.Username,
            ReportText = f.ReportType switch
            {
                ReportType.WrongQuestion => "صورت سوال اشتباه است",
                ReportType.WrongAnswer => "گزینه‌های پاسخ اشتباه هستند",
                ReportType.WrongCourse => "درس نادرست است",
                ReportType.Other => f.Description,
            }
        }).ToList();
    }
}