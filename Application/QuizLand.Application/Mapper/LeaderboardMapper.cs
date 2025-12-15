using QuizLand.Application.Contract.QueryResults.Leaderboard;
using QuizLand.Domain.Leaderboard;

namespace QuizLand.Application.Mapper;

public static class LeaderboardMapper
{
    public static List<GetWeeklyLeaderboardQueryResult> GetWeeklyLeaderboardMapper(this List<UserPeriodScore> query)
    {
       return  query.OrderByDescending(x => x.Score).Select((x, index) => new GetWeeklyLeaderboardQueryResult
            {
                UserId = x.UserId,
                Username = x.Username,
                CorrectAnswers = x.CorrectAnswers,
                GamesWon = x.GamesWon,
                Score = x.Score,
                Rank = index + 1
            })
            .ToList();
    }
    
    public static List<GetMonthlyLeaderboardQueryResult> GetMonthlyLeaderboardMapper(this List<UserPeriodScore> query)
    {
        return  query.OrderByDescending(x => x.Score).Select((x, index) => new GetMonthlyLeaderboardQueryResult
            {
                UserId = x.UserId,
                Username = x.Username,
                CorrectAnswers = x.CorrectAnswers,
                GamesWon = x.GamesWon,
                Score = x.Score,
                Rank = index + 1
            })
            .ToList();
    }
}