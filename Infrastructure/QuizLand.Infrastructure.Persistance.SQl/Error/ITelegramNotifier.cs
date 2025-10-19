namespace QuizLand.Infrastructure.Persistance.SQl.Error;

public interface ITelegramNotifier
{
    Task SendAsync(
        string level,
        string message,
        string? path = null,
        int? statusCode = null,
        string? correlationId = null,
        string? userId = null,
        DateTime? whenUtc = null);
}