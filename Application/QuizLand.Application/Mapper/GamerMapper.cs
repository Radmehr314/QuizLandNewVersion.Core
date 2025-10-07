using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;

namespace QuizLand.Application.Mapper;

public static class GamerMapper
{
    public static Gamer AddFirstGamer(this Guid userId, Game game)
    {
        return new Gamer()
        {
            IsOwner = false,
            UserId = userId,
            Game = game,
            JoinedAt = DateTime.Now,
        };
    }
    public static Gamer AddSecondGamer(this Guid userId, Guid gameId)
    {
        return new Gamer()
        {
            IsOwner = false,
            UserId = userId,
            GameId = gameId,
            JoinedAt = DateTime.Now,
        };
    }
    
}