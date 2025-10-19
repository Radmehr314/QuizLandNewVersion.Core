using QuizLand.Domain.Models.ErrorLogs;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class ErrorLogRepository : IErrorLogRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public ErrorLogRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task Add(ErrorLog errorLog)=> await _dataBaseContext.AddAsync(errorLog);
}