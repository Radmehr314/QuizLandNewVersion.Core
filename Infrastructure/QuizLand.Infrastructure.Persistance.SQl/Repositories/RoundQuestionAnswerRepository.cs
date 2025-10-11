using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.RandQuestionAnswers;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class RoundQuestionAnswerRepository :IRoundQuestionAnswerRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public RoundQuestionAnswerRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task<bool> AnswerExist(long roundId, Guid gamerId) => await _dataBaseContext.RoundQuestionAnswers.AnyAsync(f=>f.GamerId == gamerId && _dataBaseContext.RoundQuestions.Any(rq=>rq.Id == f.RoundQuestionId && rq.RoundId == roundId));

    public async Task AddRange(IEnumerable<RoundQuestionAnswer> answers) => await _dataBaseContext.RoundQuestionAnswers.AddRangeAsync(answers);
}