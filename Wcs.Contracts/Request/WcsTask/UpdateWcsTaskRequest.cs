using Wcs.Shared;

namespace Wcs.Contracts.Request.WcsTask;

public record UpdateWcsTaskRequest(
    int? Level,
    bool? IsEnforce,
    int SerialNumber,
    string? DeviceName,
    WcsTaskStatusEnum? TaskStatus,
    string? GetLocation,
    string? PutLocation);