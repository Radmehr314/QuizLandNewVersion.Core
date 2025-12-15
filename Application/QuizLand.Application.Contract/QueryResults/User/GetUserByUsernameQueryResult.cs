using QuizLand.Application.Contract.DTOs;

namespace QuizLand.Application.Contract.QueryResults.User;

public class GetUserByUsernameQueryResult
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string AvatarPath { get; set; }
    public LevelInfoDto LevelInfo { get; set; }
}