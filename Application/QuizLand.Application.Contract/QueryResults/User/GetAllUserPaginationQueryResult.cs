namespace QuizLand.Application.Contract.QueryResults.User;

public class GetAllUserPaginationQueryResult
{
    public List<GetAllUserQueryResult> AllUsers { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
}