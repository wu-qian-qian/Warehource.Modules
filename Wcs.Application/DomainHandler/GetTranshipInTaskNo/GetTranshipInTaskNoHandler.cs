using Common.Application.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.DeviceController.Tranship;

namespace Wcs.Application.DomainHandler.GetTranshipInTaskNo
{
    internal class GetTranshipInTaskNoHandler(IStackerTranshipInController _controller)
        : IEventHandler<GetTranshipInTaskNoEvent, string>
    {
        public ValueTask<string> Handle(GetTranshipInTaskNoEvent @event, CancellationToken cancellationToken = default)
        {
            var taskNo = _controller.GetWcsTaskNoByTunnle(@event.Tunnle);
            return ValueTask.FromResult(taskNo);
        }
    }
}