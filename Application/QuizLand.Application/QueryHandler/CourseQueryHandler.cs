using DocumentFormat.OpenXml.Office2010.ExcelAc;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Course;
using QuizLand.Application.Contract.QueryResults.Course;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class CourseQueryHandler : IQueryHandler<GetAllCourseQuery,List<GetAllCourseQueryResult>>,IQueryHandler<GetAllCoursePaginationQuery,GetAllCoursePaginationQueryResult>,IQueryHandler<GetCourseByIdQuery,GetCourseByIdQueryResult>
,IQueryHandler<GetAllAvailableCourseQuery,List<GetAllAvailableCourseQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CourseQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
    }
    public async Task<List<GetAllCourseQueryResult>> Handle(GetAllCourseQuery query)
    {
        var data = await _unitOfWork.CourseRepository.GetAllCourses();
        return data.GetAllCourseMapper();
    }

    public async Task<GetAllCoursePaginationQueryResult> Handle(GetAllCoursePaginationQuery query)
    {
        var data = await _unitOfWork.CourseRepository.GetAllCoursesPagination(query.pageNumber, query.size);
        var count = await _unitOfWork.CourseRepository.Count();
        return data.GetAllPaginationCourseMapper(query.size, count);
    }

    public async Task<GetCourseByIdQueryResult> Handle(GetCourseByIdQuery query)
    {
        var course =  await _unitOfWork.CourseRepository.GetById(query.Id);
        return course.GetCourseByIdMapper(); 
    }

    public async Task<List<GetAllAvailableCourseQueryResult>> Handle(GetAllAvailableCourseQuery query)
    {
        var course =  await _unitOfWork.CourseRepository.UnPickedCourses(query.GameId);
        return course.GetUnpickedCoursesMapper();
    }
}