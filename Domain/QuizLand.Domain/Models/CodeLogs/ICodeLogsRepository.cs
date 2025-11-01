namespace QuizLand.Domain.Models.CodeLogs;

public interface ICodeLogsRepository
{
    Task Add(CodeLogs codeLogs);
    Task<CodeLogs> GetByUsernameOrPhoneNumber(string usernameOrPhoneNumber , string code);
    
    Task<List<CodeLogs>> GetAllByUsername(int pageNumber, int size,string username);
    Task<long> CountByUsername(string username);
    Task<List<CodeLogs>> AllPagination(int pageNumber, int size);
    Task<long> Count();
}