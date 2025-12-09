using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.Gamers;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class GamerRepository : IGamerRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public GamerRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task Add(Gamer gamer) => await _dataBaseContext.Gamers.AddAsync(gamer);

    public async Task<Gamer?> GetById(Guid id) => await _dataBaseContext.Gamers.FirstOrDefaultAsync(f=>f.Id == id);

    public async Task<int> CountOfClients(Guid gamerId) 
    {
        var gamers = await _dataBaseContext.Gamers.Include(g => g.Game).FirstOrDefaultAsync(g => g.Id == gamerId);
        return gamers.Game.CountOfJoinedClients;
    }


    public async Task<Gamer> GetGamerByClientandGame(Guid gameId, Guid userId)=> await _dataBaseContext.Gamers.FirstOrDefaultAsync(g => g.GameId == gameId && g.UserId == userId);


    public async Task<List<Gamer>> GetGamersByGameId(Guid gameId) =>
        await _dataBaseContext.Gamers.Include(f => f.User).Where(f => f.GameId == gameId).ToListAsync();

    public async Task<Guid> GetOpponentId(Guid gameId, Guid currentGamerId)
    {
        var gs = await _dataBaseContext.Gamers.Where(g => g.GameId == gameId).Select(g => g.Id).ToListAsync();
        return gs.Single(id => id != currentGamerId);
    }

    public async Task<Guid> GetOpponentUserIdByUserAndGameId(Guid gameId, Guid userId)
    {
        var gs = await _dataBaseContext.Gamers.Where(g => g.GameId == gameId).ToListAsync();
        return gs.FirstOrDefault(id => id.UserId != userId).UserId;
    }

    public async Task<(Gamer owner, Gamer guest)> GetPlayersAsync(Guid gameId)
    {
        var gamers = await _dataBaseContext.Gamers
            .Where(g => g.GameId == gameId)
            .ToListAsync();

        var owner = gamers.Single(g => g.IsOwner);
        var guest = gamers.Single(g => !g.IsOwner);
        return (owner, guest);
    }

    public async Task DeleteAll()
    {
        var gamers = await _dataBaseContext.Gamers.ToListAsync();
        _dataBaseContext.RemoveRange(gamers);
    }
}