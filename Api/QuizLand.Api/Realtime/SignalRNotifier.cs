using Microsoft.AspNetCore.SignalR;
using QuizLand.Application.Contract.Contracts;

namespace QuizLand.Api.Hubs;

public class SignalRNotifier : IRealTimeNotifier
{
    private readonly IHubContext<NotificationHub> _hub;

    public SignalRNotifier(IHubContext<NotificationHub> hub)
    {
        _hub = hub;
    }
    public Task BroadcastAsync(string eventName, object payload, CancellationToken ct = default) => _hub.Clients.All.SendAsync(eventName, payload, ct);

    public Task SendToUserAsync(string userId, string eventName, object payload, CancellationToken ct = default) => _hub.Clients.User(userId).SendAsync(eventName, payload, ct);

}