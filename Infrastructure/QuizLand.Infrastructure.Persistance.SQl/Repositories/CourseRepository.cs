using Microsoft.EntityFrameworkCore;
using QuizLand.Domain.Dtos.Course;
using QuizLand.Domain.Models.Courses;
using QuizLand.Infrastructure.Persistance.SQl.FrameWork;

namespace QuizLand.Infrastructure.Persistance.SQl.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public CourseRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    public async Task AddCourse(Course course) => await _dataBaseContext.Courses.AddAsync(course);
    public async Task DeleteCourse(long id) => _dataBaseContext.Courses.Remove(await GetById(id));
    public async Task<List<Course>> GetAllCoursesPagination(int pageNumber, int size) => await _dataBaseContext.Courses.OrderByDescending(i => i.Id).Skip(Helper.CalculateSkip(pageNumber, size)).Take(size).ToListAsync();
    public async Task<List<Course>> GetAllCourses() => await _dataBaseContext.Courses.ToListAsync();
    public async Task<Course> GetById(long id) => await _dataBaseContext.Courses.Include(f=>f.Questions).FirstOrDefaultAsync(f=>f.Id == id);

    public Task<List<GetAvailableCoursesDto>> UnPickedCourses(Guid gameId) =>
        _dataBaseContext.Courses
            .Select(c => new GetAvailableCoursesDto
            {
                Id = c.Id,
                Title = c.Title,
                IsAvailable = !_dataBaseContext.Rounds
                    .Any(r => r.GameId == gameId && r.CourseId == c.Id)
            })
            .OrderByDescending(x => x.IsAvailable) // اول قابل‌انتخاب‌ها
            .ThenBy(x => x.Title)
            .ToListAsync();


    public async Task<long> Count() => await _dataBaseContext.Courses.CountAsync();
}