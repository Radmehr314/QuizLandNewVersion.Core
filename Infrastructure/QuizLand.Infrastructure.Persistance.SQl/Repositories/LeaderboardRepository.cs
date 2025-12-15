using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Leaderboard;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class LeaderboardRepository : ILeaderboardRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public LeaderboardRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task<List<UserPeriodScore>> GetUserScoresInPeriod(DateTime from, DateTime to)
    {
        var query = _dataBaseContext.RoundQuestionAnswers
            .Where(qa => qa.SubmitedAt >= from && qa.SubmitedAt < to)
            // اول دیتا رو صاف و ساده به یک شکل قابل گروپ شدن تبدیل می‌کنیم
            .Select(qa => new
            {
                UserId       = qa.Gamer.User.Id,
                Username     = qa.Gamer.User.Username,
                GameId       = qa.Gamer.Game.Id,
                IsCorrect    = qa.IsCorrect,
                WinnerUserId = qa.Gamer.Game.WinnerUserId
            })
            // بعد بر اساس کاربر گروه‌بندی می‌کنیم
            .GroupBy(x => new { x.UserId, x.Username })
            // و در نهایت مدل دامینی رو می‌سازیم
            .Select(g => new UserPeriodScore(
                g.Key.UserId,
                g.Key.Username,
                g.Count(x => x.IsCorrect),
                g
                    .Where(x => x.WinnerUserId == g.Key.UserId)
                    .Select(x => x.GameId)
                    .Distinct()
                    .Count()
            ));
        return await query.ToListAsync();
    }
}