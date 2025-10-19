using Microsoft.EntityFrameworkCore;
using QuizLand.Infrastructure.Persistance.SQl.FrameWork;
using QuizLand.Domain.Models.Users;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public UserRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task Add(User user)=>await _dataBaseContext.AddAsync(user);

    public async Task<User> GetById(Guid id) => await _dataBaseContext.Users.Include(f=>f.Avatar).FirstOrDefaultAsync(f=>f.Id == id);

    public async Task<List<User>> All() => await _dataBaseContext.Users.ToListAsync();
    public async Task Delete(Guid id) =>  _dataBaseContext.Remove(await GetById((id)));
    public async Task<User> CheckUserByUsernameAndPassword(string username, string password) =>
        await _dataBaseContext.Users.FirstOrDefaultAsync(
            f => f.Username == username && f.Password == password);

    public async Task<User> GetByUsername(string username) => await _dataBaseContext.Users.FirstOrDefaultAsync(f=>f.Username == username);
    public async Task<User> GetByPhoneNumber(string phoneNumber)=> await _dataBaseContext.Users.FirstOrDefaultAsync(f=>f.PhoneNumber == phoneNumber);

    public async Task<List<User>> AllPagination(int pageNumber, int size)=>await _dataBaseContext.Users.Skip(Helper.CalculateSkip(pageNumber, size)).Take(size).ToListAsync();

    public async Task<long> Count() => await _dataBaseContext.Users.LongCountAsync();
    public async Task<bool> UserExists(string username) => await _dataBaseContext.Users.AnyAsync(f => f.Username == username);


    public async Task<long> CountByOnlineUsers()=> await _dataBaseContext.Users.LongCountAsync(f => f.IsOnline);
}