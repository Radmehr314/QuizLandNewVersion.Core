namespace QuizLand.Application.Contract.QueryResults.CodeLog;

public class GetAllCodeLogByPersonalCodePaginationQueryResult
{
    public List<GetAllCodeQueryResult> AllCodeLog { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
}