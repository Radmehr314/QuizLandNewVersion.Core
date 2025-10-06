using Microsoft.EntityFrameworkCore;
using QuizLand.Infrastructure.Persistance.SQl.FrameWork;
using QuizLand.Domain.Models.CodeLogs;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class CodeLogsRepository : ICodeLogsRepository
{
    private readonly DataBaseContext _databaseContext;

    public CodeLogsRepository(DataBaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    public async Task Add(CodeLogs codeLogs) => await _databaseContext.AddAsync(codeLogs);

    public async Task<CodeLogs> GetByUsername(string username, string code) => await _databaseContext.CodeLogs.FirstOrDefaultAsync(f=>f.Username == username && code == f.Otp);
 
    public async Task<List<CodeLogs>> GetAllByUsername(int pageNumber, int size, string username) => await _databaseContext.CodeLogs.Where(f=>f.Username == username).Skip(Helper.CalculateSkip(pageNumber, size)).Take(size).ToListAsync();

    public async Task<long> CountByUsername(string username) => await _databaseContext.CodeLogs.Where(f=>f.Username == username).LongCountAsync();

    public async Task<List<CodeLogs>> GetAllByUsername(string username) =>
        await _databaseContext.CodeLogs.ToListAsync();

    public async Task<List<CodeLogs>> AllPagination(int pageNumber, int size) => await _databaseContext.CodeLogs.Skip(Helper.CalculateSkip(pageNumber, size)).Take(size).ToListAsync();

    public async Task<long> Count() => await _databaseContext.CodeLogs.LongCountAsync();
}