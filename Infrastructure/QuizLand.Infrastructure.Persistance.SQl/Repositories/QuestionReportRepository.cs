using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.QuestionReports;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class QuestionReportRepository : IQuestionReportRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public QuestionReportRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task Add(QuestionReport questionReport) => await _dataBaseContext.AddAsync(questionReport);

    public async Task<List<QuestionReport>> GetAll() => await _dataBaseContext.QuestionReports.Include(f=> f.User).ToListAsync();
    public async Task<List<QuestionReport>> GetAllBy(long id) => await _dataBaseContext.QuestionReports.Where(f => f.Id == id).ToListAsync();
}