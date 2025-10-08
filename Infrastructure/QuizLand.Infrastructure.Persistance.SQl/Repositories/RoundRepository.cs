using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.Rands;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class RoundRepository : IRoundRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public RoundRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task Add(Round round) => await _dataBaseContext.AddAsync(round);
    public async Task<Round?> GetByGameAndNumber(Guid gameId, int roundNumber) => await _dataBaseContext.Rounds.FirstOrDefaultAsync(f=>f.GameId == gameId&& f.RoundNumber == roundNumber);
}