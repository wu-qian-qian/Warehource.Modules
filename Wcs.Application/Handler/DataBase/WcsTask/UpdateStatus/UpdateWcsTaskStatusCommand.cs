using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.WcsTask.UpdateStatus;

public class UpdateWcsTaskStatusCommand : ICommand<Result<string>>
{
    public int SerialNumber { get; set; }

    public WcsTaskStatusEnum WcsTaskStatusType { get; set; }
}