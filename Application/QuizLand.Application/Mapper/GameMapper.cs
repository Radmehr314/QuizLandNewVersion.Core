using QuizLand.Application.Contract.Commands.Game;
using QuizLand.Application.Contract.Queries.Game;
using QuizLand.Application.Contract.QueryResults.Game;
using QuizLand.Domain.Models.Games;

namespace QuizLand.Application.Mapper;

public static class GameMapper
{
    public static Game TwoPlayerFactory(this StartTwoPlayerGameCommand command)
    {
        return new Game()
        {
            Type = 1,
            CountOfJoinedClients = 1,
            StartedAt = DateTime.Now,
            MatchClients = false,
            RoundNumber = 1
        };
    }

    public static List<GetAllMyRunningGamesQueryResult> GetAllMyRunningGamesMapper(
        this List<Game> games, Guid userId)
    {
        var nowDate = DateTime.Now;

        return games.Select(f =>
        {
            // StartedAt را سریع به UTC ببریم (Local → UTC ، Unspecified هم مثل Local فرض می‌شود)
            var startedAtDate = f.StartedAt;

            var elapsed = nowDate - startedAtDate;
            if (elapsed < TimeSpan.Zero) elapsed = TimeSpan.Zero;

            return new GetAllMyRunningGamesQueryResult
            {
                Id = f.Id,
                Type = f.Type,
                StartedAt = f.StartedAt,
                CountOfJoinedClients = f.CountOfJoinedClients,
                RoundNumber = f.RoundNumber,
                IsYourTurn = (f.UserTurnId != null && f.UserTurnId == userId),
                SpentHours = (int)elapsed.TotalHours,                 // کل ساعت‌ها
                SpentMinutes = (int)(elapsed.TotalMinutes % 60),       // دقیقه‌ی باقیمانده 0..59
                OpponentAvatar = (f.CountOfJoinedClients == 2 && f.Type == 1 ? f.Gamers.Where(f=>f.UserId != userId).FirstOrDefault().User.Avatar.FilePath.Replace('\\', '/')  :f.Gamers.Where(f=>f.UserId == userId).FirstOrDefault().User.Avatar.FilePath.Replace('\\', '/')  ),
                OpponentUsername = (f.CountOfJoinedClients == 2 ? f.Gamers.Where(f=>f.UserId != userId).FirstOrDefault().User.Username : "در انتظار حریف")
            };
        }).ToList();
    }


    private static string ToAgoText(TimeSpan ts)
    {
        if (ts.TotalSeconds < 60) return $"{ts.Seconds} ثانیه پیش";
        if (ts.TotalMinutes < 60) return $"{(int)ts.TotalMinutes} دقیقه پیش";
        if (ts.TotalHours   < 24) return $"{(int)ts.TotalHours} ساعت پیش";
        return $"{(int)ts.TotalDays} روز پیش";
    }

    public static GetGameByIdQueryResult GetGameByIdMapper(this Game game,Guid userId)
    {
        return new GetGameByIdQueryResult()
        {
            Id = game.Id,
            Type = game.Type,
            CountOfJoinedClients = game.CountOfJoinedClients,
            StartedAt = game.StartedAt,
            MatchClients = game.MatchClients,
            EndedAt = game.EndedAt,
            WinnerClientId = game.WinnerUserId,
            RoundNumber = game.RoundNumber,
            IsYourTurn = (game.UserTurnId != null && game.UserTurnId == userId ? true : false),
            Gamers = game.GetGamersByGameIdMapper()
        };
    }
}