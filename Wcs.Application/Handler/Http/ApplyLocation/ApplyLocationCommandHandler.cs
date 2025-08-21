using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Wcs.Application.Handler.Http.ApplyLocation;

public class ApplyLocationCommandHandler : ICommandHandler<ApplyLocationCommand, Result<string>>
{
    public Task<Result<string>> Handle(ApplyLocationCommand request, CancellationToken cancellationToken)
    {
        //TODO 向上游申请库位
        Result<string> result = new();
        result.SetValue("1_1_1");
        return Task.FromResult(result);
    }
}