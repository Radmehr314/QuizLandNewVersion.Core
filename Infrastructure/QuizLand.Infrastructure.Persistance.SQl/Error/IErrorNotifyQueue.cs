namespace QuizLand.Infrastructure.Persistance.SQl.Error;

public interface IErrorNotifyQueue
{
    ValueTask EnqueueAsync(ErrorNotifyItem item, CancellationToken ct = default);
    ValueTask<ErrorNotifyItem> DequeueAsync(CancellationToken ct);
}
public sealed class ErrorNotifyItem
{
    public long Id { get; set; }
    public string Level { get; set; } = "Error";
    public string Message { get; set; } = default!;
    public string? Path { get; set; }
    public int? StatusCode { get; set; }
    public string? CorrelationId { get; set; }
    public DateTime WhenUtc { get; set; }
}
