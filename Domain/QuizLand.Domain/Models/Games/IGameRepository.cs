
namespace QuizLand.Domain.Models.Games;

public interface IGameRepository
{
    Task Add(Game game);
    Task<List<Game>> GetAll();
    Task<List<Game>> GetAllMyRunningGames(Guid userId);

    Task<Game> CheckOpenGameToJoin();
    Task<Game> GetGameById(Guid id);
    Task<Game?> Match(Guid userId);
    Task<bool> CanStartNewGame(Guid userId);

}