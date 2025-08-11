using MassTransit;
using Plc.CustomEvents;
using Plc.CustomEvents.Saga;
using Wcs.CustomEvents;

namespace Plc.Presentation.Saga;

/// <summary>
///     分布式事务Saga
/// </summary>
public class PlcMapSaga : MassTransitStateMachine<PlcMapState>
{
    public PlcMapSaga()
    {
        // 绑定状态存储字段
        InstanceState(x => x.CurrentState);

        // 配置事件关联规则（通过DeviceName关联同一个设备的事件）
        Event(() => PlcMapCreated, x =>
        {
            x.CorrelateBy(saga => saga.DeviceName, context => context.Message.DeviceName);
            x.SelectId(context => Guid.NewGuid()); // 为新实例生成唯一标识
        });

        Event(() => PlcMapDataIntegrationCompleted,
            x => { x.CorrelateBy(saga => saga.DeviceName, context => context.Message.DeviceName); });
        Event(() => PlcMapProcessed, x =>
        {
            // 通过 DeviceName 关联到对应的 Saga 实例
            x.CorrelateBy(saga => saga.DeviceName, context => context.Message.DeviceName);
        });
        // 初始流程：第一次收到PlcMapCreated事件
        Initially(When(PlcMapCreated)
            .Then(context =>
            {
                // 通过context.Saga访问当前Saga实例的状态对象
                context.Saga.DeviceName = context.Message.DeviceName;
            })
            // 发布数据集成服务调用事件
            .Publish(context =>
                new PlcMapDataIntegrationEvent(DateTime.Now, context.Message.DeviceName, context.Message.EntityNames))
            // 进入"处理中"状态，等待服务结果
            .TransitionTo(Processing));

        #region 判断逻辑处理

        //// 处理中状态：等待数据集成服务的结果反馈
        //During(Processing,
        //    When(PlcMapDataIntegrationCompleted)
        //        // 根据服务处理结果分支处理
        //        .IfElse(context => context.Message.Success,
        //            // 处理成功分支
        //            then => then
        //                // 切换到Pending状态
        //                .TransitionTo(Pending)
        //                // 发布处理成功的最终事件
        //                .Publish(context => new PlcMapProcessed(
        //                    context.Saga.DeviceName,
        //                     true)),
        //            // 处理失败分支
        //            then => then
        //                // 切换到Pending状态
        //                .TransitionTo(Paid)
        //                // 发布处理成功的最终事件
        //                .Publish(context => new PlcMapProcessed(
        //                    context.Saga.DeviceName,
        //                     false))
        //        )
        //);

        #endregion

        // 处理中状态：等待数据集成服务的结果反馈
        During(Processing,
            When(PlcMapDataIntegrationCompleted)
                .Publish(context => new PlcMap.PlcMapProcessed(
                    context.Saga.DeviceName,
                    context.Message.Success)).TransitionTo(Pending));

        // 可以在这里添加对最终状态的后续处理（如超时控制等） 如果object是强一致性就需要用消息队列
        DuringAny(
            When(PlcMapProcessed)
                .Publish(context => new PlcMapEventCommitEvent(context.Message.DeviceName, context.Message.Success))
                .Finalize() // 结束Saga实例
        );
    } // 定义状态

    public State Processing { get; } // 处理中（等待服务结果）
    public State Pending { get; } // 处理成功状态

    // 定义事件
    public Event<PlcMap.PlcMapCreated> PlcMapCreated { get; }
    public Event<PlcMap.PlcMapDataIntegrationCompleted> PlcMapDataIntegrationCompleted { get; }
    public Event<PlcMap.PlcMapProcessed> PlcMapProcessed { get; }
}