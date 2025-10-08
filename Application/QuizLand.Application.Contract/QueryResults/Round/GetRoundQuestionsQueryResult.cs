using QuizLand.Application.Contract.QueryResults.RoundQuestion;

namespace QuizLand.Application.Contract.QueryResults.Round;

public class GetRoundQuestionsQueryResult
{
    public long RoundId { get; set; }
    public int RoundNumber { get; set; }
    public IEnumerable<GetAllRoundQuestionQueryResult> Questions { get; set; } = Array.Empty<GetAllRoundQuestionQueryResult>();
}