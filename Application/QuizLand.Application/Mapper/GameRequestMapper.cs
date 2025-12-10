using QuizLand.Application.Contract.Commands.GameRequest;
using QuizLand.Domain.Models.GameRequests;

namespace QuizLand.Application.Mapper;

public static class GameRequestMapper
{
    public static GameRequest Factory(this SendNewGameRequestCommand command,Guid userId)
    {
        return new GameRequest()
        {
            FirstUserId = userId,
            SecondUserId = command.userId,
            GameRequestStatus = GameRequestStatus.Pending,
            CreatedAt = DateTime.Now,
        };
    }
}