using System.Threading.Tasks;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.CodeLog;
using QuizLand.Application.Contract.Queries.User;
using QuizLand.Application.Contract.QueryResults.CodeLog;
using QuizLand.Application.Contract.QueryResults.User;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class CodeLogQueryHandler :  IQueryHandler<GetAllCodeLogPaginationQuery,GetAllCodeLogPaginationQueryResult>
,IQueryHandler<GetAllCodeLogByPersonalCodePaginationQuery,GetAllCodeLogByPersonalCodePaginationQueryResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public CodeLogQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

 

    public async Task<GetAllCodeLogPaginationQueryResult> Handle(GetAllCodeLogPaginationQuery query)
    {
        var results  = await _unitOfWork.CodeLogsRepository.AllPagination(query.pageNumber, query.size);
        var count = await _unitOfWork.CodeLogsRepository.Count();
        return results.AllPagination(query.size, count);

    }

    public async Task<GetAllCodeLogByPersonalCodePaginationQueryResult> Handle(GetAllCodeLogByPersonalCodePaginationQuery query)
    {
        var results  = await _unitOfWork.CodeLogsRepository.GetAllByUsername(query.pageNumber, query.size,query.Username);
        var count = await _unitOfWork.CodeLogsRepository.CountByUsername(query.Username);
        return results.AllByPersonalcodePagination(query.size, count);
    }
}