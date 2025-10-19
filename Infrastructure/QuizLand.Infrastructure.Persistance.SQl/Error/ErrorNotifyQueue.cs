using System.Threading.Channels;

namespace QuizLand.Infrastructure.Persistance.SQl.Error;

public class ErrorNotifyQueue: IErrorNotifyQueue
{
    private readonly Channel<ErrorNotifyItem> _channel = Channel.CreateUnbounded<ErrorNotifyItem>();
    public ValueTask EnqueueAsync(ErrorNotifyItem item, CancellationToken ct = default)
        => _channel.Writer.WriteAsync(item, ct);
    public ValueTask<ErrorNotifyItem> DequeueAsync(CancellationToken ct)
        => _channel.Reader.ReadAsync(ct);
}