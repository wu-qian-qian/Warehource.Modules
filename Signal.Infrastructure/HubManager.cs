using Microsoft.AspNetCore.SignalR;
using SignalR.Application;
using SignalR.Application.Client;
using SignalR.Application.ChatHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Infrastructure
{
    /// <summary>
    /// HubManager 类实现了 IHubManager 接口，用于管理 SignalR Hub 的消息发送功能。
    /// </summary>
    internal class HubManager:IHubManager
    {
        //周期单例
        private readonly IHubContext<WcsHub,IClient> _hubContext;

        public HubManager(IHubContext<WcsHub, IClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendUserMessage(string userName,string content)
        {
            var userId = WcsHub.UserConnections.FirstOrDefault(x => x.Key == userName).Value;
            await _hubContext.Clients.User(userId).SendMessage(content);
        }
        public async Task SendAllUserMessage(string content)
        {
            await _hubContext.Clients.All.SendMessage(content);
        }
    }
}
