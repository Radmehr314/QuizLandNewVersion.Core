using QuizLand.Application.Contract.QueryResults.Gamer;
using QuizLand.Application.Contract.QueryResults.RoundQuestionAnswer;

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
    public bool IsYourTurn { get; set; }


    public int CountOfJoinedClients { get; set; }
    public IEnumerable<GetGamersByGameIdQueryResult> Gamers { get; set; }
    public IEnumerable<GetAllRoundQuestionAnswerQueryResult>? RoundQuestionAnswers { get; set; }
}