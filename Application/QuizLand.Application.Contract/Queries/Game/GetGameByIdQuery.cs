using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Game;

public class GetGameByIdQuery : IQuery
{
    public Guid GameId { get; set; }
}