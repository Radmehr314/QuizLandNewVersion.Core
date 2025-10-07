namespace QuizLand.Application.Contract.Commands.Gamer;

public class AddGamerCommand
{
    public DateTime CreatedDateTime { get; set; }
    public Guid GameId { get; set; }
    public Guid UserId { get; set; }
    public bool IsOwner { get; set; }
}