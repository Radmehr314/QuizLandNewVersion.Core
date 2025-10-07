using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Models.Questions;
using QuizLand.Infrastructure.Persistance.SQl.FrameWork;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class QuestionRepository : IQuestionRepository
{
    protected readonly DataBaseContext _dataBaseContext;

    public QuestionRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task Add(Question question) => await _dataBaseContext.Questions.AddAsync(question);

    public async Task<List<Question>> GetAllPagination(long courseid, int pageNumber, int size) => await _dataBaseContext.Questions.Where(x => x.CourseId == courseid).OrderByDescending(i => i.Id).Skip(Helper.CalculateSkip(pageNumber, size)).Take(size).ToListAsync();
    public async Task<List<Question>> GetAll(long courseid) => await _dataBaseContext.Questions.Where(x => x.CourseId == courseid).ToListAsync();

    public async Task Delete(long id) => _dataBaseContext.Questions.Remove(await GetById(id));
    public async Task<Question> GetById(long id) => await _dataBaseContext.Questions.Include(f=>f.Course).FirstOrDefaultAsync(f=>f.Id == id);

    public async Task<long> Count() => await _dataBaseContext.Questions.CountAsync();
}