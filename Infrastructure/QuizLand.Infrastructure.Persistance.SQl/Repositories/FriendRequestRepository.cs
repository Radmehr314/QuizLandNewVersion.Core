using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.FriendRequests;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class FriendRequestRepository:IFriendRequestRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public FriendRequestRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task SendNewRequest(FriendRequest friendRequest) => await _dataBaseContext.AddAsync(friendRequest);

    public async Task<FriendRequest> GetById(long id) => await _dataBaseContext.FriendRequests.FindAsync(id);

    public async Task<List<FriendRequest>> AllMyReuests(Guid userId) => await _dataBaseContext.FriendRequests.Include(f=>f.Requester)
        .Where(f => f.ReceiverId == userId &&
                    f.FriendRequestStatus == FriendRequestStatus.Pending).ToListAsync();
}