using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;
using QuizLand.Domain.Models.Rands;

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
    public async Task<List<Game>> GetAllMyRunningGames(Guid userId) => await _dataBaseContext.Games.Include(f=>f.Gamers).ThenInclude(f=>f.User).ThenInclude(f=>f.Avatar).Where(f=>f.Gamers.Any(g=>g.UserId == userId) && f.EndedAt == null).ToListAsync();

    public async Task<Game> CheckOpenGameToJoin() =>
        await _dataBaseContext.Games.FirstOrDefaultAsync(f => f.MatchClients == false);

    public async Task<Game> GetGameById(Guid id) => await _dataBaseContext.Games.Include(f=>f.Gamers).ThenInclude(f=>f.User).ThenInclude(f=>f.Avatar).Include(f=>f.Rounds).ThenInclude(f=>f.FirstRoundQuestion).ThenInclude(f=>f.RoundQuestionAnswers).Include(f=>f.Rounds).ThenInclude(f=>f.SecondRoundQuestion).ThenInclude(f=>f.RoundQuestionAnswers).Include(f=>f.Rounds).ThenInclude(f=>f.ThirdRoundQuestion).ThenInclude(f=>f.RoundQuestionAnswers).Include(f=>f.Rounds).ThenInclude(f=>f.Course).FirstOrDefaultAsync(f => f.Id == id);
    public async Task<Game?> Match(Guid userId) => await _dataBaseContext.Games.Include(f=>f.Rounds).Include(g => g.Gamers).ThenInclude(f=>f.User).FirstOrDefaultAsync(g =>
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

    public async Task<IGameRepository.GameCorrectAnswerResult> GetWinnerByCorrectAnswers(Guid gameId)
    {
        var gamers = await _dataBaseContext.Gamers
            .Where(g => g.GameId == gameId)
            .ToListAsync();

        var owner = gamers.Single(g => g.IsOwner);
        var guest = gamers.Single(g => !g.IsOwner);
        
        var totals = await _dataBaseContext.RoundQuestionAnswers.Include(f=>f.RoundQuestion)
            .Where(a =>
                a.RoundQuestion.Round.GameId == gameId &&
                (a.RoundQuestion.Round.RoundStatus == RoundStatus.Completed))
            .GroupBy(a => a.GamerId)
            .Select(g => new
            {
                GamerId = g.Key,
                Correct = g.Count(x => x.IsCorrect)
            })
            .ToListAsync();
        
        var ownerCorrect = totals.FirstOrDefault(t => t.GamerId == owner.Id)?.Correct ?? 0;
        var guestCorrect = totals.FirstOrDefault(t => t.GamerId == guest.Id)?.Correct ?? 0;
        Guid? winnerUserId = null;
        
        if (ownerCorrect != guestCorrect)
            winnerUserId = ownerCorrect > guestCorrect ? owner.UserId : guest.UserId;
        
        return new IGameRepository.GameCorrectAnswerResult(
            owner, ownerCorrect,
            guest, guestCorrect,
            winnerUserId
        );
    }

    public async Task DeleteAll()
    {
        var games = await _dataBaseContext.Games.ToListAsync();
        _dataBaseContext.RemoveRange(games);
    }
}