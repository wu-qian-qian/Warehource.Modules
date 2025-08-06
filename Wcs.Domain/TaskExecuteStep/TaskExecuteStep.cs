using Common.Domain.EF;

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
    /// </summary>
    public bool IsSend { get; set; }

    public string? Description { get; set; }

    /// <summary>
    ///     路径组用来区分行走的路劲
    /// </summary>
    public string? PathNodeGroup { get; set; }

    /// <summary>
    ///     当前节点
    /// </summary>
    public Guid? ExecuteNodePath { get; set; }
}