using MassTransit;
using MediatR;
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
                //将Send值修改为false 触发重新写入
                await send.Send(context.Message.Key);
            }
            //TODO 触发事件更新任务执行节点任务数据
        }
    }
}