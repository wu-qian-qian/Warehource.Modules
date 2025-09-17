using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Shared;

namespace Wcs.Application.Handler.Http.ApplyLocation;

public class ApplyLocationCommandHandler : ICommandHandler<ApplyLocationCommand, Result<string>>
{
    public Task<Result<string>> Handle(ApplyLocationCommand request, CancellationToken cancellationToken)
    {
        //TODO 向上游申请库位
        Result<string> result = new();
        if (request.CreatorSystemType == CreatorSystemTypeEnum.Other)
            result.SetValue("1_1_1");
        else
            result.SetValue("无需解析");

        return Task.FromResult(result);
    }
}