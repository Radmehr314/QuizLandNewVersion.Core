using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.QuestionReport;
using QuizLand.Application.Contract.QueryResults.QuestionReport;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class QuestionReportQueryHandler : IQueryHandler<GetAllQuestionReportQuery,List<GetAllQuestionReportQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionReportQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<GetAllQuestionReportQueryResult>> Handle(GetAllQuestionReportQuery query)
    {
       var data  =  await _unitOfWork.QuestionReportRepository.GetAll();
       return data.GetAllMapper();
    }
}