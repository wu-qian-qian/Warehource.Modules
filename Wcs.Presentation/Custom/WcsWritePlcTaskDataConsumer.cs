using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.CustomEvents;

namespace Wcs.Presentation.Custom
{
    /// <summary>
    /// 状态机更新状态如果写入数据失败，状态机任务状态，重新写入数据
    /// </summary>
    /// <param name="send"></param>
    public class WcsWritePlcTaskDataConsumer(ISender send) : IConsumer<WcsWritePlcTaskDataIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<WcsWritePlcTaskDataIntegrationEvent> context)
        {
            if (context.Message.IsSucess == false)
            {
                await send.Send(context.Message.Key);
            }
        }
    }
}