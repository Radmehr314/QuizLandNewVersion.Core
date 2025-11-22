using QuizLand.Domain.Models.QuestionReports;

namespace QuizLand.Application.Contract.QueryResults.QuestionReport;

public class GetAllQuestionReportQueryResult
{
    public long QuestionId { get; set; }
    public string ReportText { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; }
}