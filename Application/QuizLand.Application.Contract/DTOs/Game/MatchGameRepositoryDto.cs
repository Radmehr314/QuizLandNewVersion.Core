namespace QuizLand.Application.Contract.DTOs.Game;

public class MatchGameRepositoryDto
{
    public string? Username { get; set; }
    public bool IsMatch { get; set; }
    public Guid? gameId { get; set; }
}