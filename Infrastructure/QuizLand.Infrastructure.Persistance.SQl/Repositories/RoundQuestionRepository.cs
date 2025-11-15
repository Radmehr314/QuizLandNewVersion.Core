using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.Questions;
using QuizLand.Domain.Models.RandQuestions;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class RoundQuestionRepository : IRoundQuestionRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public RoundQuestionRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task Add(RoundQuestion roundQuestion) => await _dataBaseContext.RoundQuestions.AddAsync(roundQuestion);
    public async Task<List<RoundQuestion>> LoadQuestionsAsync(long roundId) => await _dataBaseContext.RoundQuestions.Where(rq => rq.RoundId == roundId)
        .OrderBy(rq => rq.QuestionNumber).Select(rq => new RoundQuestion
        {
            Id = rq.Id,
            QuestionNumber = rq.QuestionNumber,
            Question = new Question()
            {
                Id = rq.Question.Id,
                QuestionTitle = rq.Question.QuestionTitle,
                FirstOption = rq.Question.FirstOption,
                SecondOption = rq.Question.SecondOption,
                ThirdOption = rq.Question.ThirdOption,
                Source = rq.Question.Source,
                DescriptiveAnswer = rq.Question.DescriptiveAnswer,
                CorrectOption = rq.Question.CorrectOption,
                CountClickFirstOption = rq.Question.CountClickFirstOption,
                CountClickSecondOption = rq.Question.CountClickSecondOption,
                CountClickThirdOption = rq.Question.CountClickThirdOption,
                CountClickFourthOption = rq.Question.CountClickFourthOption,
            }
        }).ToListAsync();
}