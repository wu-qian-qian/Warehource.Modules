using System.Collections.Concurrent;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Wcs.Application.Handler.DataBase.Job.Get;

namespace Wcs.Application.SignalR.ChatHub;

[Authorize]
public class WcsHub(ISender sender) : Hub<IClient>
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
        var data = await sender.Send(new GetAllJobQuery());
        await Task.Delay(100);
        Console.WriteLine(message);
        await Clients.All.ReceiveMessage("服务器回信...." + message);
    }
}