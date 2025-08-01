﻿using Common.Domain.EF;
using Wcs.Shared;

namespace Wcs.Domain.TaskExecuteStep;

/// <summary>
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

    public int StepIndex { get; set; }

    public string? Description { get; set; }

    public Guid? ExecuteNodePath { get; set; }

    /// <summary>
    ///     区域
    /// </summary>
    public Guid? RegionId { get; set; }

    /// <summary>
    ///     任务类型
    /// </summary>
    public WcsTaskTypeEnum WcsTaskType { get; set; }
}