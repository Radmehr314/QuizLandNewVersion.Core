using QuizLand.Application.Contract.Commands.Game;
using QuizLand.Application.Contract.Queries.Game;
using QuizLand.Application.Contract.QueryResults.Game;
using QuizLand.Domain.Models.Games;

namespace QuizLand.Application.Mapper;

public static class GameMapper
{
    public static Game TwoPlayerFactory(this StartTwoPlayerGameCommand command)
    {
        return new Game()
        {
            Type = 1,
            CountOfJoinedClients = 1,
            StartedAt = DateTime.Now,
            MatchClients = false,
        };
    }

    public static List<GetAllMyRunningGamesQueryResult> GetAllMyRunningGamesMapper(this List<Game> games)
    {
        return games.Select(f => new GetAllMyRunningGamesQueryResult() {Id = f.Id,Type = f.Type,StartedAt = f.StartedAt,CountOfJoinedClients = f.CountOfJoinedClients}).ToList();
    }

    public static GetGameByIdQueryResult GetGameByIdMapper(this Game game)
    {
        return new GetGameByIdQueryResult()
        {
            Id = game.Id,
            Type = game.Type,
            CountOfJoinedClients = game.CountOfJoinedClients,
            StartedAt = game.StartedAt,
            MatchClients = game.MatchClients,
            EndedAt = game.EndedAt,
            WinnerClientId = game.WinnerUserId,
            Gamers = game.GetGamersByGameIdMapper()
        };
    }
}