using Common.Domain.EF;
using Wcs.Shared;

namespace Wcs.Domain.TaskExecuteStep;

/// <summary>
///     一条任务一条执行步骤
///     任务执行步骤
///     每种任务类型对应一类执行步骤
///     执行方式--》(获取执行任务，执行任务拿到执行设备《通过区域筛选；如一个巷道一个区域》，执行设备执行该条任务)
///     上一设备执行完成--》更新任务执行步骤，再次上面操作知道最后一个执行步骤
/// </summary>
public class TaskExecuteStep : IEntity
{
    public TaskExecuteStep() : base(Guid.NewGuid())
    {
    }

    /// <summary>
    ///     是否发送设备
    ///     发送，发送中，发送结束，成功
    ///     如果我发送结束说明发送失败需要重新推送
    /// </summary>
    public TaskExecuteStepTypeEnum TaskExecuteStepType { get; set; }

    public string? Description { get; set; }

    /// <summary>
    ///     路径组用来区分行走的路劲
    /// </summary>
    public string? PathNodeGroup { get; set; }

    public string? CurentDevice { get; set; }

    public DeviceTypeEnum? DeviceType { get; set; }
}