namespace QuizLand.Application.Contract.QueryResults.FriendRequest;

public class AllMyFriendRequestsQueryResult
{
    public long Id { get; set; }
    public Guid RequesterId { get; set; }
    public string RequesterUsername { get; set; }
    public string SendedAt { get; set; }
}