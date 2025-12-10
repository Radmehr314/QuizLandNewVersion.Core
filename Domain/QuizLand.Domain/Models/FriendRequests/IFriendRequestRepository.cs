namespace QuizLand.Domain.Models.FriendRequests;

public interface IFriendRequestRepository
{
    Task SendNewRequest(FriendRequest friendRequest);
    Task<FriendRequest> GetById(long id);
    Task<List<FriendRequest>> AllMyReuests(Guid userId);
    Task<bool> Exist(Guid requesterId, Guid reciverId);
}