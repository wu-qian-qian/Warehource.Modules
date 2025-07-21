using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Application
{
    public interface IHubManager
    {

        public Task SendUserMessage(string userName, string content);
        public Task SendAllUserMessage(string content);
    }
}
