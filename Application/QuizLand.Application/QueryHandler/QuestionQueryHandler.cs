using System.Numerics;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Course;
using QuizLand.Application.Contract.Queries.Question;
using QuizLand.Application.Contract.QueryResults.Course;
using QuizLand.Application.Contract.QueryResults.Question;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class QuestionQueryHandler : IQueryHandler<GetByIdQuestionQuery, GetByIdQuestionQueryResult>,IQueryHandler<GetAllQuestionQuery,List<GetAllQuestionQueryResult>>,IQueryHandler<GetAllQuestionPaginationQuery,GetAllQuestionPaginationQueryResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetByIdQuestionQueryResult> Handle(GetByIdQuestionQuery query)
    {
        var question =  await _unitOfWork.QuestionRepository.GetById(query.Id);
        return question.GetByIdQuestionMapper();
    }

    public async Task<List<GetAllQuestionQueryResult>> Handle(GetAllQuestionQuery query)
    {
        var questions = await _unitOfWork.QuestionRepository.GetAll(query.CourseId);
        return questions.GetAllQuestionMapper();
    }

    public async Task<GetAllQuestionPaginationQueryResult> Handle(GetAllQuestionPaginationQuery query)
    {
        var questions =
            await _unitOfWork.QuestionRepository.GetAllPagination(query.CourseId, query.pageNumber, query.size);
        var count = await _unitOfWork.QuestionRepository.Count();
        return questions.GetAllQuestionPaginationMapper(query.size,count);
    }
}