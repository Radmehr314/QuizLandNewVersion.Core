namespace QuizLand.Domain.Leaderboard;

public class UserPeriodScore
{
    public Guid UserId { get; }
    public string Username { get; }
    public int CorrectAnswers { get; }
    public int GamesWon { get; }

    // فرمول امتیاز
    public int Score => CorrectAnswers * 10 + GamesWon * 50;

    public UserPeriodScore(Guid userId, string username, int correctAnswers, int gamesWon)
    {
        UserId = userId;
        Username = username;
        CorrectAnswers = correctAnswers;
        GamesWon = gamesWon;
    }
}