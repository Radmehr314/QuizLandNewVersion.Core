using QuizLand.Application.Contract.DTOs;
using QuizLand.Application.Contract.QueryResults.Friend;
using QuizLand.Domain.Models.FriendRequests;
using QuizLand.Domain.Models.Friends;

namespace QuizLand.Application.Mapper;

public static class FriendMapper
{
    public static Friend Factory(this FriendRequest friendRequest)
    {
       return new Friend()
        {
            FirstUserId = friendRequest.RequesterId,
            SecondUserId = friendRequest.ReceiverId,
            CreatedAt = DateTime.Now
        };
    }

    public static List<AllMyFriendQueryResult> AllMyFriend(this List<Friend> friends,Guid userId,int BaseXp,int GrowXp)
    {
        return friends.Select(f => new AllMyFriendQueryResult()
        {
            Id = f.Id,
            Username = f.FirstUserId == userId ? f.SecondUser.Username : f.FirstUser.Username,
            Avatar = f.FirstUserId == userId ? f.SecondUser.Avatar.FilePath : f.FirstUser.Avatar.FilePath,
            LevelInfo = f.FirstUserId == userId ? CalculateLevelInfo(f.FirstUser.XP,BaseXp,GrowXp) : CalculateLevelInfo(f.SecondUser.XP,BaseXp,GrowXp) 
        }).ToList();
    }
    
    
    public static LevelInfoDto CalculateLevelInfo(long totalXp,int BaseXp,int GrowXp)
    {
        var level = 1;
        var xpLeft = totalXp;
        
        while (true)
        {
            // XP لازم برای رفتن از این لول به لول بعد
            var costForThisLevel = BaseXp + GrowXp * (level - 1);

            if (xpLeft < costForThisLevel)
            {
                // اینجا دیگه نمی‌تونه بره لول بعد، پس این لول فعلیه
                var xpInCurrentLevel = xpLeft;
                var xpNeedForNextLevel = costForThisLevel;
                var remainingForNextLevel = xpNeedForNextLevel - xpInCurrentLevel;

                var progress = xpNeedForNextLevel == 0
                    ? 0
                    : (double)xpInCurrentLevel / xpNeedForNextLevel;

                return new LevelInfoDto
                {
                    TotalXp = totalXp,
                    Level = level,
                    XpInCurrentLevel = xpInCurrentLevel,
                    XpNeedForNextLevel = xpNeedForNextLevel,
                    RemainingForNextLevel = remainingForNextLevel,
                };
            }

            // هنوز XP کافی دارد، می‌ره لول بعد
            xpLeft -= costForThisLevel;
            level++;
        }
    }

}