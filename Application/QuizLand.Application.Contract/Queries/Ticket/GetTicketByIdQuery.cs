using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Ticket;

public class GetTicketByIdQuery : IQuery
{
    public long Id { get; set; }
}