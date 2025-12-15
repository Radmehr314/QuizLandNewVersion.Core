namespace QuizLand.Domain.Leaderboard;

public interface ILeaderboardRepository
{
    Task<List<UserPeriodScore>> GetUserScoresInPeriod(DateTime from, DateTime to);
}