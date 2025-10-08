using QuizLand.Application.Contract.QueryResults.Gamer;
using QuizLand.Domain.Models.Gamers;
using QuizLand.Domain.Models.Games;

namespace QuizLand.Application.Mapper;

public static class GamerMapper
{
    public static Gamer AddFirstGamer(this Guid userId, Game game)
    {
        return new Gamer()
        {
            IsOwner = true,
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

    public static List<GetGamersByGameIdQueryResult> GetGamersByGameIdMapper(this Game game) => game.Gamers.Select(f=>new GetGamersByGameIdQueryResult(){Id = f.Id,GameId = f.GameId,IsOwner = f.IsOwner,JoiedAt = f.JoinedAt,UserId = f.UserId,Username = f.User.Username,AvatarPath = f.User.Avatar.FilePath.Replace('\\', '/')}).ToList();
    
    
}