using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Leaderboard;
using QuizLand.Application.Contract.QueryResults.Leaderboard;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class LeaderboardQueryHandler : IQueryHandler<GetMonthlyLeaderboardQuery,List<GetMonthlyLeaderboardQueryResult>>,IQueryHandler<GetWeeklyLeaderboardQuery,List<GetWeeklyLeaderboardQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public LeaderboardQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<GetMonthlyLeaderboardQueryResult>> Handle(GetMonthlyLeaderboardQuery query)
    {
        var today = DateTime.UtcNow;
        var from = new DateTime(today.Year, today.Month, 1);
        var to = from.AddMonths(1);
        var leaderboarddata = await _unitOfWork.LeaderboardRepository.GetUserScoresInPeriod(from, to);
        return leaderboarddata.GetMonthlyLeaderboardMapper();
    }

    public async Task<List<GetWeeklyLeaderboardQueryResult>> Handle(GetWeeklyLeaderboardQuery query)
    {
        var today = DateTime.UtcNow;
        var from = today.Date.AddDays(-7);
        var to = today.Date.AddDays(1);
        var leaderboarddata = await _unitOfWork.LeaderboardRepository.GetUserScoresInPeriod(from, to);
        return leaderboarddata.GetWeeklyLeaderboardMapper();

    }
}