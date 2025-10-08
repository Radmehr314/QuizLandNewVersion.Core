using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.Avatars;
using QuizLand.Infrastructure.Persistance.SQl.FrameWork;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class AvatarRepository : IAvatarRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public AvatarRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task Add(Avatar avatar) => await _dataBaseContext.Avatars.AddAsync(avatar);
    public async Task<Avatar> GetById(long id) => await _dataBaseContext.Avatars.FindAsync(id);
    public async Task<Avatar> GetFirst() => await _dataBaseContext.Avatars.FirstOrDefaultAsync();

    public async Task Delete(long id) =>  _dataBaseContext.Avatars.Remove(await GetById(id));

    public async Task<List<Avatar>> GetAll() => await _dataBaseContext.Avatars.ToListAsync();

    public async Task<List<Avatar>> GetAllPagination(int pageNumber, int size) => await _dataBaseContext.Avatars.Skip(Helper.CalculateSkip(pageNumber, size)).Take(size).ToListAsync();
    public async Task<long> Count() => await _dataBaseContext.Avatars.LongCountAsync();
}