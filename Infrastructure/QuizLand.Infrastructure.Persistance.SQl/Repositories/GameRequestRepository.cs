using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.GameRequests;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class GameRequestRepository : IGameRequestRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public GameRequestRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task SendGameRequest(GameRequest gameRequest) => await _dataBaseContext.AddAsync(gameRequest);

    public async Task<GameRequest> GetById(long id) => await _dataBaseContext.GameRequests.FindAsync(id);

    public async Task<List<GameRequest>> AllMyGameRequest(Guid UserId) => await _dataBaseContext.GameRequests
        .Where(f => f.FirstUserId == UserId || f.SecondUserId == UserId).ToListAsync();

    public async Task<bool> Exist(Guid firstUser, Guid secondUser) => await _dataBaseContext.GameRequests.AnyAsync(f =>
        (f.FirstUserId == firstUser && f.SecondUserId == secondUser && f.GameRequestStatus ==  GameRequestStatus.Pending) ||
        (f.FirstUserId == secondUser && f.SecondUserId == firstUser && f.GameRequestStatus ==  GameRequestStatus.Pending));
}