namespace QuizLand.Application.Contract.QueryResults.Gamer;

public class GetGamersByGameIdQueryResult
{
    public Guid Id { get; set; }
    public DateTime JoiedAt { get; set; }
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string AvatarPath { get; set; }
    public bool IsOwner { get; set; }
}