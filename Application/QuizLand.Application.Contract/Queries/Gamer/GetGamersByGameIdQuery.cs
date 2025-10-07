using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Queries.Gamer;

public class GetGamersByGameIdQuery : IQuery
{
    public Guid GameId { get; set; }
}