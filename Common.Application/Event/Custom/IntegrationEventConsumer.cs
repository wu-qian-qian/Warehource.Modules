using Common.Domain.Event;
using MassTransit;

namespace Common.Application.Event.Custom;

/// <summary>
///     MassTransit 集成事件消费者
///     公共使用例如发送一个MQ事件
/// </summary>
/// <typeparam name="TIntegrationEvent"></typeparam>
public class IntegrationEventConsumer<TIntegrationEvent> : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IMassTransitDomainEvent
{
    public Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        //  throw new NotImplementedException();
        Console.WriteLine("发送一个MQ事件");
        Console.WriteLine("发送一个封电子邮件");
        return Task.CompletedTask;
    }
}