using QuizLand.Application.Contract.Queries.CodeLog;

namespace QuizLand.Application.Contract.QueryResults.CodeLog;

public class GetAllCodeLogPaginationQueryResult
{
    public List<GetAllCodeQueryResult> AllCodeLog { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
}