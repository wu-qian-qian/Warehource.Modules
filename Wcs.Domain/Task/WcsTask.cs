using Common.Domain.EF;
using Wcs.Shared;

namespace Wcs.Domain.Task;

public class WcsTask : IEntity
{
    public WcsTask() : base(Guid.NewGuid())
    {
        IsRead = false;
    }

    public string TaskCode { get; set; }

    /// <summary>
    ///     流水号
    /// </summary>
    public int SerialNumber { get; set; }

    /// <summary>
    ///     任务类型
    /// </summary>
    public WcsTaskTypeEnum TaskType { get; set; }

    /// <summary>
    ///     任务状态
    /// </summary>
    public WcsTaskStatusEnum TaskStatus { get; set; }

    /// <summary>
    ///     任务描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     取货库位 层-排-列
    /// </summary>
    public GetLocation GetLocation { get; set; }

    /// <summary>
    ///     放货库位 层-排-列
    /// </summary>
    public PutLocation PutLocation { get; set; }

    public bool IsRead { get; set; }

    public Guid? DeviceId { get; set; }

    public Guid? RegionId { get; set; }

    /// <summary>
    ///     任务执行步骤
    /// </summary>
    public Guid TaskExecuteStepId { get; set; }

    public TaskExecuteStep.TaskExecuteStep TaskExecuteStep { get; set; }
}