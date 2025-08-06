using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Shared;

namespace Wcs.Application.DBHandler.WcsTask.UpdateStatus;

public class UpdateWcsTaskStatusEvent : ICommand<Result<string>>
{
    public int SerialNumber { get; set; }

    public WcsTaskStatusEnum WcsTaskStatusType { get; set; }
}