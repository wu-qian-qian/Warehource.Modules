using Common.Application.MediatR.Message;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.Business.FilterStackerTask;

public class FilterStackerCommand : ICommand<WcsTask>
{
    public IEnumerable<WcsTask> WcsTasks { get; set; }

    public bool IsTranShipPoint { get; set; }
}