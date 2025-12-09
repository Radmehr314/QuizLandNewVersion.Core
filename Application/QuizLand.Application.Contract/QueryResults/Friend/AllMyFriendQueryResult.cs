using QuizLand.Application.Contract.DTOs;

namespace QuizLand.Application.Contract.QueryResults.Friend;

public class AllMyFriendQueryResult
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Avatar { get; set; } 
    public LevelInfoDto LevelInfo { get; set; }

}