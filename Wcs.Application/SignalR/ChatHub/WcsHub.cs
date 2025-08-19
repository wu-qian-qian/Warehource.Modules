using System.Collections.Concurrent;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Wcs.Application.SignalR.ChatHub;

[Authorize]
public class WcsHub : Hub<IClient>
{
    public static ConcurrentDictionary<string, string> UserConnections { get; } = new();


    public override Task OnConnectedAsync()
    {
        var userName = Context.User.FindFirstValue(ClaimTypes.Name);
        if (!string.IsNullOrEmpty(userName)) UserConnections[userName] = Context.ConnectionId;
        return base.OnConnectedAsync();
    }

    public async Task SendMessage(string message)
    {
        Console.WriteLine(message);
    }
}