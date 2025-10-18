using QuizLand.Application.Contract.QueryResults.Gamer;

namespace QuizLand.Application.Contract.QueryResults.Game;

public class GetGameByIdQueryResult
{
    public Guid Id { get; set; }
    public int Type { get; set; }
    public int RoundNumber { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public Guid? WinnerClientId { get; set; }
    public bool MatchClients { get; set; }

    public int CountOfJoinedClients { get; set; }
    public IEnumerable<GetGamersByGameIdQueryResult> Gamers { get; set; }
}