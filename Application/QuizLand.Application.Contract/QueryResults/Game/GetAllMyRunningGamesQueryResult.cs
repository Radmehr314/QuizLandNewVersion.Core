namespace QuizLand.Application.Contract.QueryResults.Game;

public class GetAllMyRunningGamesQueryResult
{
    public Guid Id { get; set; }
    public int Type { get; set; }
    public DateTime StartedAt { get; set; }
    public int CountOfJoinedClients { get; set; }
}