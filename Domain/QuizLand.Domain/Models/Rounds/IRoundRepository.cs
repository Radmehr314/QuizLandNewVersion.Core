namespace QuizLand.Domain.Models.Rands;

public interface IRoundRepository
{
    Task Add(Round round);
    Task<Round?> GetByGameAndNumber(Guid gameId, int roundNumber);
}