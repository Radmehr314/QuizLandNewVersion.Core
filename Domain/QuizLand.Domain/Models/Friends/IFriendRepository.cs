namespace QuizLand.Domain.Models.Friends;

public interface IFriendRepository
{
    Task Add(Friend friend);
    Task<List<Friend>> AllMyFriend(Guid userId);
}