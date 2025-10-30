using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Shared;

namespace Wcs.Contracts.Business
{
    public record WcsTaskModel(
        Guid id,
        WcsTaskStatusEnum WcsTaskStatus,
        TaskExecuteStepTypeEnum TaskExecuteStepType,
        string? PathNodeGroup,
        string? CurentDevice,
        DeviceTypeEnum? DeviceType);
}