using QuizLand.Application.Contract.Commands.Game;
using QuizLand.Application.Contract.Commands.Gamer;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.DTOs.Game;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Mapper;
using QuizLand.Domain;
using QuizLand.Domain.Models.Games;
using QuizLand.Domain.Models.Rands;

namespace QuizLand.Application.CommandHandler;

public class GameCommandHandler  :ICommandHandler<StartTwoPlayerGameCommand>,ICommandHandler<StartOnePlayerGameCommand>,ICommandHandler<StartGameWithFriendCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IRealTimeNotifier _realTimeNotifier;


    public GameCommandHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService, IRealTimeNotifier realTimeNotifier)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _realTimeNotifier = realTimeNotifier;
    }
    public async Task<CommandResult> Handle(StartTwoPlayerGameCommand command)
    {
        var userId = _userInfoService.GetUserIdByToken();
        var MatchFound = await Match(userId);
        if (MatchFound.IsMatch)
        {
            if (MatchFound.Username == "Error") throw new NotFoundException("خطا در اتصال!!!");
            var gameMatchedDto = new GameMatchedMessageDto()
            {
                GameId = MatchFound.gameId.Value,
                Username = MatchFound.Username,
            };

            var oppenentId =
                await _unitOfWork.GamerRepository.GetOpponentUserIdByUserAndGameId(MatchFound.gameId.Value, userId);
            
            await _realTimeNotifier.SendToUserAsync(oppenentId.ToString(),"GameMatched",new {GameInfo = gameMatchedDto, at = DateTime.UtcNow});
            return new CommandResult() { Id = MatchFound.gameId };
        }
         var newGameId = await GameGeneratorForTwoPlayer(command, userId);

        return new CommandResult() { Id = newGameId };
    }
    
    
    public async Task<MatchGameRepositoryDto> Match(Guid UserId)
    {
        var game = await _unitOfWork.GameRepository.Match(UserId);
        string opponentUsername = "Error";
        if (game == null)
        {
            return new MatchGameRepositoryDto(){IsMatch = false};
        }
        game.CountOfJoinedClients = 2;
        game.MatchClients = true;
        if (game.RoundNumber == 1 && game.Rounds.FirstOrDefault().RoundStatus == RoundStatus.AwaitingP2)
        {
            game.UserTurnId = UserId;
        }

        var opponent = await _unitOfWork.UserRepository.GetById(UserId);
        opponent.Coin -= 5;
        if (opponent != null && opponent != null)
        {
            opponentUsername = opponent.Username;
        }
     
        await _unitOfWork.GamerRepository.Add(UserId.AddSecondGamer(game.Id));
        await _unitOfWork.Save();
        return new MatchGameRepositoryDto(){IsMatch = true,Username = opponentUsername,gameId = game.Id};
    }
    
    private async Task<Guid> GameGeneratorForTwoPlayer(StartTwoPlayerGameCommand startTwoPlayerGameCommand, Guid userId)
    {
        var canStartNewGame = await _unitOfWork.GameRepository.CanStartNewGame(userId);
        if (!canStartNewGame) throw new ValidationException("شما 5  بازی در حال اجرا دارید!!!");
        var game = startTwoPlayerGameCommand.TwoPlayerFactory();
        var gamer = userId.AddFirstGamer(game);
        var round = game.RoundFactory(gamer);

        game.UserTurnId = _userInfoService.GetUserIdByToken();
        await _unitOfWork.GameRepository.Add(game);
        await _unitOfWork.GamerRepository.Add(gamer);
        await _unitOfWork.RoundRepository.Add(round);
        var user  =  await _unitOfWork.UserRepository.GetById(userId);
        user.Coin -= 5;

        await _unitOfWork.Save();
        return game.Id;
    }
    
    private async Task<Guid> GameGeneratorForPlayWithFriend(StartGameWithFriendCommand startTwoPlayerGameCommand, Guid userId)
    {
        var game = startTwoPlayerGameCommand.PlayeWithFriendFactory();
        var gamer = userId.AddFirstGamer(game);
        var round = game.RoundFactory(gamer);

        game.UserTurnId = _userInfoService.GetUserIdByToken();
        await _unitOfWork.GameRepository.Add(game);
        await _unitOfWork.GamerRepository.Add(gamer);
        await _unitOfWork.RoundRepository.Add(round);
        var user  =  await _unitOfWork.UserRepository.GetById(userId);
        user.Coin -= 5;

        await _unitOfWork.Save();
        return game.Id;
    }

    public async Task<CommandResult> Handle(StartOnePlayerGameCommand command)
    {
        var userId = _userInfoService.GetUserIdByToken();
        var canStartNewGame = await _unitOfWork.GameRepository.CanStartNewGame(userId);
        if (!canStartNewGame) throw new ValidationException("شما 5  بازی در حال اجرا دارید!!!");
        var game = command.OnePlayerFactory();
        var gamer = userId.AddFirstGamer(game);
        var round = game.RoundFactory(gamer);

        game.UserTurnId = _userInfoService.GetUserIdByToken();
        await _unitOfWork.GameRepository.Add(game);
        await _unitOfWork.GamerRepository.Add(gamer);
        await _unitOfWork.RoundRepository.Add(round);
        var user  =  await _unitOfWork.UserRepository.GetById(userId);
        user.Coin -= 5;

        await _unitOfWork.Save();
        return new CommandResult() { Id = game.Id };

    }

    public async Task<CommandResult> Handle(StartGameWithFriendCommand command)
    {
        var userId = _userInfoService.GetUserIdByToken();
        var MatchFound = await Match(userId);
        var newGameId = await GameGeneratorForPlayWithFriend(command, userId);
        return new CommandResult() { Id = newGameId };
    }
}