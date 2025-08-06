using Wcs.Shared;

namespace Wcs.Contracts.Request.WcsTask;

public class GetWcsTaskRequest
{
    public string? Container { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? TaskCode { get; set; }

    public int? SerialNumber { get; set; }

    public string? GetLocation { get; set; }

    public string? PutLocation { get; set; }

    /// <summary>
    ///     任务状态
    /// </summary>
    public WcsTaskStatusEnum TaskStatus { get; set; } = WcsTaskStatusEnum.Created;

    /// <summary>
    ///     任务状态
    /// </summary>
    public CreatorSystemTypeEnum? CreatorSystemType { get; set; }
}