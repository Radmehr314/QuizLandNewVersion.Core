namespace QuizLand.Application.Contract.QueryResults.Leaderboard;

public class GetMonthlyLeaderboardQueryResult
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public int CorrectAnswers { get; set; }
    public int GamesWon { get; set; }
    public int Score { get; set; }
    public int Rank { get; set; }
}