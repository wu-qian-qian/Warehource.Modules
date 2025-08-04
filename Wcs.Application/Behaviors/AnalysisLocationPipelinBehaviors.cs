using MediatR;
using Wcs.Application.DBHandler.WcsTask.Insert;

namespace Wcs.Application.Behaviors;

public class AnalysisLocationPipelinBehaviors<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : InsertWcsTaskEvent
{
    //根据规则解析出适配于交互的货位
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        return next();
    }
}