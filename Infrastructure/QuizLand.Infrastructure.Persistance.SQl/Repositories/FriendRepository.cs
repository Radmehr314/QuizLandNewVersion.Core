using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.Friends;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class FriendRepository : IFriendRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public FriendRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task Add(Friend friend) => await _dataBaseContext.AddAsync(friend);

    public async Task<List<Friend>> AllMyFriend(Guid userId) => await _dataBaseContext.Friends.Include(f=>f.FirstUser).ThenInclude(f=>f.Avatar).Include(f=>f.SecondUser).ThenInclude(f=>f.Avatar)
        .Where(f => f.FirstUserId == userId || f.SecondUserId == userId).ToListAsync();
}