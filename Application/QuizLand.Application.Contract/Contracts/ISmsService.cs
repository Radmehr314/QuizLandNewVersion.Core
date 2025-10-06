namespace QuizLand.Application.Contract.Contracts;

public interface ISmsService
{
    Task<bool> SendCode(string mobile,string code);
}