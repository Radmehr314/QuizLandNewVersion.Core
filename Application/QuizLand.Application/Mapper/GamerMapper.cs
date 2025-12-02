using Microsoft.Extensions.Configuration;
using QuizLand.Application.Contract.DTOs;
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

    public static List<GetGamersByGameIdQueryResult> GetGamersByGameIdMapper(this Game game,int BaseXp,int GrowXp) => game.Gamers.Select(f=>new GetGamersByGameIdQueryResult(){Id = f.Id,GameId = f.GameId,IsOwner = f.IsOwner,JoiedAt = f.JoinedAt,UserId = f.UserId,Username = f.User.Username,Level = CalculateLevelInfo(f.User.XP,BaseXp,GrowXp).Level,AvatarPath = f.User.Avatar.FilePath.Replace('\\', '/')}).ToList();
    
    
    public static LevelInfoDto CalculateLevelInfo(long totalXp,int BaseXp,int GrowXp)
    {
        var level = 1;
        var xpLeft = totalXp;


        while (true)
        {
            // XP لازم برای رفتن از این لول به لول بعد
            var costForThisLevel = BaseXp + GrowXp * (level - 1);

            if (xpLeft < costForThisLevel)
            {
                // اینجا دیگه نمی‌تونه بره لول بعد، پس این لول فعلیه
                var xpInCurrentLevel = xpLeft;
                var xpNeedForNextLevel = costForThisLevel;
                var remainingForNextLevel = xpNeedForNextLevel - xpInCurrentLevel;

                var progress = xpNeedForNextLevel == 0
                    ? 0
                    : (double)xpInCurrentLevel / xpNeedForNextLevel;

                return new LevelInfoDto
                {
                    TotalXp = totalXp,
                    Level = level,
                    XpInCurrentLevel = xpInCurrentLevel,
                    XpNeedForNextLevel = xpNeedForNextLevel,
                    RemainingForNextLevel = remainingForNextLevel,
                };
            }

            // هنوز XP کافی دارد، می‌ره لول بعد
            xpLeft -= costForThisLevel;
            level++;
        }
    }

    
}