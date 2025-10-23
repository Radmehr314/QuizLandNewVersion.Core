using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Game;
using QuizLand.Application.Contract.QueryResults.Game;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class GameQueryHandler : IQueryHandler<GetAllMyRunningGamesQuery,List<GetAllMyRunningGamesQueryResult>>,IQueryHandler<GetGameByIdQuery,GetGameByIdQueryResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public GameQueryHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }
    public async Task<List<GetAllMyRunningGamesQueryResult>> Handle(GetAllMyRunningGamesQuery query)
    {
        var games = await _unitOfWork.GameRepository.GetAllMyRunningGames(_userInfoService.GetUserIdByToken());
        return games.GetAllMyRunningGamesMapper(_userInfoService.GetUserIdByToken());

    }

    public async Task<GetGameByIdQueryResult> Handle(GetGameByIdQuery query)
    {
        var game = await _unitOfWork.GameRepository.GetGameById(query.GameId);
        if (game == null) throw new NotFoundException("بازی یافت نشد!!!");
        var data =  game.GetGameByIdMapper(_userInfoService.GetUserIdByToken());
        return data;
    }
}