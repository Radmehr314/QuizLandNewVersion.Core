namespace QuizLand.Application.Contract.Exceptions;

public class ValidationException : Exception
{
    public string Errors { get; }

    public ValidationException(string errors)
        : base("یک یا چند فیلد به درستی مقدار دهی نشده است!!!")
    {
        Errors = errors;
    }
}