using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR.Application.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Application.ChatHub
{
    [Authorize]
    public class WcsHub : Hub<IClient>
    {
        public static ConcurrentDictionary<string, string> UserConnections { get; private set; }

        public WcsHub()
        {
            UserConnections = new ConcurrentDictionary<string, string>();
        }
        
        public override Task OnConnectedAsync()
        {
            string userName = Context.User.FindFirstValue(ClaimTypes.Name);
            if (!string.IsNullOrEmpty(userName))
            {
                UserConnections[userName] = Context.ConnectionId;
            }
            return base.OnConnectedAsync();
        }
    }
}
