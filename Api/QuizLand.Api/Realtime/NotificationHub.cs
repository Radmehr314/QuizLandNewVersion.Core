using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace QuizLand.Api.Hubs;

public class NotificationHub : Hub
{
    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"[HUB] ConnId={Context.ConnectionId}  UserIdentifier={Context.UserIdentifier ?? "(null)"}");
        foreach (var c in Context.User?.Claims ?? Enumerable.Empty<Claim>())
            Console.WriteLine($"[HUB] Claim: {c.Type} = {c.Value}");
        return base.OnConnectedAsync();
    }

    public Task Echo(string message) => Clients.Caller.SendAsync("Echo", message);
}