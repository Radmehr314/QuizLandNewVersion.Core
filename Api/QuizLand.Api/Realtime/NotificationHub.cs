using Microsoft.AspNetCore.SignalR;

namespace QuizLand.Api.Hubs;

public class NotificationHub : Hub
{
    public Task Echo(string message) => Clients.Caller.SendAsync("Echo", message);
}