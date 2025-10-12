namespace QuizLand.Domain.Models.Gamers;

public interface IGamerRepository
{
    Task Add(Gamer gamer);
    Task<Gamer?> GetById(Guid id);
    Task<int> CountOfClients(Guid gamerId);
    Task<Gamer> GetGamerByClientandGame(Guid gameId, Guid userId);
    Task<List<Gamer>> GetGamersByGameId(Guid gameId);
    Task<Guid> GetOpponentId(Guid gameId, Guid currentGamerId);
    Task<(Gamer owner, Gamer guest)> GetPlayersAsync(Guid gameId);
}