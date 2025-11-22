using QuizLand.Application.Contract.Framework;
using QuizLand.Domain.Models.QuestionReports;

namespace QuizLand.Application.Contract.Commands.QuestionReport;

public class AddQuestionReportCommand : ICommand
{
    public long QuestionId { get; set; }
    public ReportType ReportType { get; set; }
    public string? Description { get; set; }
}