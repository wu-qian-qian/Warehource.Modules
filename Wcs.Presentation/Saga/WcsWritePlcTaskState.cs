using MassTransit;

namespace Wcs.Presentation.Saga;

public class WcsWritePlcTaskState : SagaStateMachineInstance, ISagaVersion
{
    // 当前状态（由MassTransit自动管理）
    public string CurrentState { get; set; }

    // 设备唯一标识（用于关联事件）
    public string DeviceName { get; set; }

    // 版本号（用于并发控制）
    public int Version { get; set; }

    // Saga实例唯一标识
    public Guid CorrelationId { get; set; }
}