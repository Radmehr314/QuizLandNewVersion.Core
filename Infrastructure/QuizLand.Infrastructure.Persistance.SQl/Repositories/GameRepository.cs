using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class GameRepository : IGameRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public GameRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task Add(Game game) => await _dataBaseContext.Games.AddAsync(game);

    public async Task<List<Game>> GetAll() => await _dataBaseContext.Games.ToListAsync();
    public async Task<List<Game>> GetAllMyRunningGames(Guid userId) => await _dataBaseContext.Games.Include(f=>f.Gamers).Where(f=>f.Gamers.Any(g=>g.UserId == userId) && f.EndedAt == null).ToListAsync();

    public async Task<Game> CheckOpenGameToJoin() =>
        await _dataBaseContext.Games.FirstOrDefaultAsync(f => f.MatchClients == false);

    public async Task<Game> GetGameById(Guid id) => await _dataBaseContext.Games.Include(f=>f.Gamers).ThenInclude(f=>f.User).ThenInclude(f=>f.Avatar).FirstOrDefaultAsync(f => f.Id == id);
    public async Task<Game?> Match(Guid userId) => await _dataBaseContext.Games.Include(g => g.Gamers).ThenInclude(f=>f.User).FirstOrDefaultAsync(g =>
            g.CountOfJoinedClients == 1 && g.Type == 1 && g.Gamers.Any(gamer => gamer.UserId != userId));

    public async Task<bool> CanStartNewGame(Guid userId)
    {
        var AllClientGames = await _dataBaseContext.Games.Include(g => g.Gamers)
            .Where(g => g.Gamers.Any(gg => gg.UserId == userId) && g.WinnerUserId == null).CountAsync();
        return AllClientGames >= 5 ? false : true;
    }

    public async Task CompleteGame(Guid gameId, Guid? winnerUserId)
    {
        var game = await _dataBaseContext.Games.FirstAsync(g => g.Id == gameId);
        game.EndedAt = DateTime.Now;
        game.WinnerUserId = winnerUserId;
    }
}