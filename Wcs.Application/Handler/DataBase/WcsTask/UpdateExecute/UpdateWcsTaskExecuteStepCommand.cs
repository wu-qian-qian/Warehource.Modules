using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.WcsTask.UpdateExecute;

public class UpdateWcsTaskExecuteStepCommand : ICommand<Result<string>>
{
    public int SerialNumber { get; set; }

    public string DeviceName { get; set; }

    public DeviceTypeEnum DeviceType { get; set; }
}