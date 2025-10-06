using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Ticket;

public class GetAllTicketPaginationQuery : IQuery
{
    public int size { get; set; }
    public int pageNumber { get; set; }
}