using Microsoft.Extensions.Configuration;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.DTOs;
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
    private readonly IConfiguration _config;


    public GameQueryHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _config = config;
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
        var BaseXp = Convert.ToInt32(_config["Leveling:BaseXp"]
                                     ?? throw new InvalidOperationException("Missing Leveling:BaseXp"));
        var GrowXp =Convert.ToInt32(_config["Leveling:GrowXp"]
                                    ?? throw new InvalidOperationException("Missing Leveling:GrowXp"));
        var data =  game.GetGameByIdMapper(_userInfoService.GetUserIdByToken(),BaseXp,GrowXp);
        return data;
    }
    

}