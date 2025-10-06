using System.Threading.Tasks;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Ticket;
using QuizLand.Application.Contract.QueryResults.Ticket;
using QuizLand.Application.Contract.QueryResults.TicketMesage;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class TicketQueryHandler : IQueryHandler<GetAllTicketPaginationQuery, GetAllTicketPaginationQueryResult>,IQueryHandler<GetTicketByIdQuery,GetTicketByIdQueryResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public TicketQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetAllTicketPaginationQueryResult> Handle(GetAllTicketPaginationQuery query)
    {
        var tickts = await _unitOfWork.TicketRepository.AllPagination(query.pageNumber, query.size);
        var count = await _unitOfWork.TicketRepository.Count();
        return tickts.AllPagination(query.size, count);
    }

    public async Task<GetTicketByIdQueryResult> Handle(GetTicketByIdQuery query)
    {
        var ticket =  await _unitOfWork.TicketRepository.GetById(query.Id);
        return ticket.GetByIdMapper();
    }
}