using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Game;

public class GetGameStateQuery : IQuery
{
    public Guid GameId { get; set; }
}