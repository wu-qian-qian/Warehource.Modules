using Wcs.Shared;

namespace Wcs.Contracts.Request.WcsTask;

public record UpdateWcsTaskRequest
{
    public int SerialNumber { get; set; }

    public string DeviceName { get; set; }

    public WcsTaskStatusEnum WcsTaskStatusType { get; set; }
}