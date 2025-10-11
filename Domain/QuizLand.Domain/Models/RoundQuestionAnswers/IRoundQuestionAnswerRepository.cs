namespace QuizLand.Domain.Models.RandQuestionAnswers;

public interface IRoundQuestionAnswerRepository
{
    Task<bool> AnswerExist(long roundId, Guid gamerId); 
    Task AddRange(IEnumerable<RoundQuestionAnswer> answers);
}