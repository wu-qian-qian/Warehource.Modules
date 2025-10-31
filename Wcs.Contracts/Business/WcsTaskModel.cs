using Wcs.Shared;

namespace Wcs.Contracts.Business;

public record WcsTaskModel(
    Guid id,
    WcsTaskStatusEnum WcsTaskStatus,
    TaskExecuteStepTypeEnum TaskExecuteStepType,
    string? PathNodeGroup,
    string? CurentDevice,
    DeviceTypeEnum? DeviceType);