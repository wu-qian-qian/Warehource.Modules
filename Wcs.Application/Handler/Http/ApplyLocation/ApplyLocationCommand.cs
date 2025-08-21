using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.Http.ApplyLocation;

public class ApplyLocationCommand : ICommand<Result<string>>
{
    public string TaskCode { get; set; }
}