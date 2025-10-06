namespace QuizLand.Application.Contract.Exceptions;

public class UserAccessException: Exception
{
    public UserAccessException(string message) : base(message) { }
}