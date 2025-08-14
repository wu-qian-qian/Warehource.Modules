using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.DB.WcsTask.UpdateExecute;

public class UpdateWcsTaskExecuteStepCommand : ICommand<Result<string>>
{
    public int SerialNumber { get; set; }

    public string DeviceName { get; set; }
}