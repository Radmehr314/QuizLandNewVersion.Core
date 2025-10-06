using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.Supporters;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class SupporterRepository : ISupporterRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public SupporterRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task Add(Supporter supporter) => await _dataBaseContext.AddAsync(supporter);

    public async Task<Supporter> GetById(Guid id) => await _dataBaseContext.Supporters.FirstOrDefaultAsync(f=>f.Id == id);
    public async Task<Supporter> GetByUsername(string username) => await _dataBaseContext.Supporters.FirstOrDefaultAsync(f => f.Username == username);

    public async Task<List<Supporter>> All() => await _dataBaseContext.Supporters.ToListAsync();

    public async Task<Supporter> CheckSupporterByUsernameAndPassword(string username, string password) => await _dataBaseContext.Supporters.FirstOrDefaultAsync(f => f.Username == username && f.Password == password);
    public async Task<bool> UsernameExists(string username) => await _dataBaseContext.Supporters.AnyAsync(f => f.Username == username);
}