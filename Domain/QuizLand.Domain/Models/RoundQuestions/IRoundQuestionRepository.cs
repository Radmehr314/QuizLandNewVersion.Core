namespace QuizLand.Domain.Models.RandQuestions;

public interface IRoundQuestionRepository
{
    Task Add(RoundQuestion roundQuestion);
    Task<List<RoundQuestion>> LoadQuestionsAsync(long roundId);
}