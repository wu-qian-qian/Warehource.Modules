using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.DBHandler.WcsTask.UpdateExecute;

public class UpdateWcsTaskExecuteStepEvent : ICommand<Result<string>>
{
    public int SerialNumber { get; set; }

    public string DeviceName { get; set; }
}