using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.Business.GetStackerStation;

public class GetStackerStaionCommand : ICommand<Result<bool>>
{
    public string Region { get; set; }

    public WcsTask WcsTask { get; set; }
}