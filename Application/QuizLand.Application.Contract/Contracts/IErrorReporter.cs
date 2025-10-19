using QuizLand.Application.Contract.Commands.ErrorLog;

namespace QuizLand.Application.Contract.Contracts;

public interface IErrorReporter
{
    Task ReportAsync(AddErrorLogCommand report);
}