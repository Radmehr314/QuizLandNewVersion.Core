namespace QuizLand.Domain.Models.CodeLogs;

public interface ICodeLogsRepository
{
    Task Add(CodeLogs codeLogs);
    Task<CodeLogs> GetByUsername(string username , string code);
    
    Task<List<CodeLogs>> GetAllByUsername(int pageNumber, int size,string username);
    Task<long> CountByUsername(string username);
    Task<List<CodeLogs>> AllPagination(int pageNumber, int size);
    Task<long> Count();
}