namespace QuizLand.Application.Contract.Contracts;

public interface IRealTimeNotifier
{
    Task BroadcastAsync(string eventName, object payload, CancellationToken ct = default);
    Task SendToUserAsync(string userId, string eventName, object payload, CancellationToken ct = default);
}