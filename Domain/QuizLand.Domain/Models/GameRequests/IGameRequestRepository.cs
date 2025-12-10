namespace QuizLand.Domain.Models.GameRequests;

public interface IGameRequestRepository
{
    Task SendGameRequest(GameRequest gameRequest);
    Task<GameRequest> GetById(long id);
    Task<List<GameRequest>> AllMyGameRequest(Guid UserId);
    Task<bool> Exist(Guid firstUser,Guid secondUser);
}