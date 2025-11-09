using QuizLand.Domain.Dtos.Course;

namespace QuizLand.Domain.Models.Courses;

public interface ICourseRepository
{
    Task AddCourse(Course course);
    Task DeleteCourse(long id);
    Task<List<Course>> GetAllCoursesPagination(int pageNumber, int size);
    Task<Course> GetById(long id);
    Task<List<GetAvailableCoursesDto>> UnPickedCourses(Guid gameId);
    Task<long> Count();
    Task<List<Course>> GetAllCourses();
}