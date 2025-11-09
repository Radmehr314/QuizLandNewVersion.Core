using QuizLand.Application.Contract.Commands.Course;
using QuizLand.Application.Contract.QueryResults.Course;
using QuizLand.Domain.Dtos.Course;
using QuizLand.Domain.Models.Courses;

namespace QuizLand.Application.Mapper;

public static class CourseMapper
{
    public static Course Factory(this AddCourseCommand command)
    {
        return new Course()
        {
            Title = command.Title
        };
    }

    public static List<GetAllCourseQueryResult> GetAllCourseMapper(this List<Course> courses)
    {
        return courses.Select(f => new GetAllCourseQueryResult() { Id = f.Id, Title = f.Title }).ToList();
    }
 

    public static GetAllCoursePaginationQueryResult GetAllPaginationCourseMapper(this List<Course> courses,int pageSize,long count)
    {
        return new GetAllCoursePaginationQueryResult()
        {
            AllCourseQueryResults = courses.GetAllCourseMapper(),
            PageSize = pageSize,
            Count = count
        };
    }
    
    public static GetCourseByIdQueryResult GetCourseByIdMapper(this Course course)
    {
        return new GetCourseByIdQueryResult()
        {
             Id = course.Id,
             Title = course.Title
        };
    }
    
    public static List<GetAllAvailableCourseQueryResult> GetUnpickedCoursesMapper(this List<GetAvailableCoursesDto> courses)
    {
        return courses.Select(f => new GetAllAvailableCourseQueryResult() { Id = f.Id, Title = f.Title,IsAvailable = f.IsAvailable}).ToList();

    }
    
}