namespace QuizLand.Domain.Models.ErrorLogs;

public interface IErrorLogRepository
{
    Task Add(ErrorLog errorLog);
}