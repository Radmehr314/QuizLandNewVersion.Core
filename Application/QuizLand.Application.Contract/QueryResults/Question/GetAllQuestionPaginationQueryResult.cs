namespace QuizLand.Application.Contract.QueryResults.Question;

public class GetAllQuestionPaginationQueryResult
{
    public List<GetAllQuestionQueryResult> AllQuestionPaginationQueryResults { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
}