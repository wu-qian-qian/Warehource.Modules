using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.Business.CheckExecuteNode;

public class CheckExecuteNodeCommand : ICommand<Result<bool>>
{
    public WcsTask WcsTask { get; set; }

    public string DeviceRegionCode { get; set; }
}