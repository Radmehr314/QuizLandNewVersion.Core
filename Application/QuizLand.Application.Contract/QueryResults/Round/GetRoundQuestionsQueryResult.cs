using QuizLand.Application.Contract.QueryResults.RoundQuestion;

namespace QuizLand.Application.Contract.QueryResults.Round;

public class GetRoundQuestionsQueryResult
{
    public long RoundId { get; set; }
    public long? CourseId { get; set; }
    public int RoundNumber { get; set; }
    public bool IsYourTurn { get; set; }    // برای UI موبایل
    public IEnumerable<GetAllRoundQuestionQueryResult> Questions { get; set; } = Array.Empty<GetAllRoundQuestionQueryResult>();
}