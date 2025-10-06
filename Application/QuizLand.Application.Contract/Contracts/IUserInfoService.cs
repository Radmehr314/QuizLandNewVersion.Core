namespace QuizLand.Application.Contract.Contracts;

public interface IUserInfoService
{
    Guid GetUserIdByToken();
}