namespace QuizLand.Application.Contract.QueryResults.Ticket;

public class GetAllTicketPaginationQueryResult
{
    public List<GetAllTicketQueryResult> AllTicketQueryResults { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
}