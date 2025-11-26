namespace QuizLand.Application.Contract.QueryResults.RoundQuestionAnswer;

public class GetAllRoundQuestionAnswerQueryResult
{
    public Guid UserId { get; set; }
    public int RoundNumber { get; set; }
    public bool IsFirstQuestionCorrect { get; set; }
    public bool IsSecondQuestionCorrect { get; set; }
    public bool IsThirdQuestionCorrect { get; set; }
}