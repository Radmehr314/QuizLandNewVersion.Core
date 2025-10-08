namespace QuizLand.Application.Contract.QueryResults.Avatar;

public class AllAvatarPaginationQeuryResult
{
    public List<AllAvatarQueryResult> AllAvatar { get; set; }
    public int PageSize { get; set; }
    public long Count { get; set; }
}