using MediatR;
using Plc.Application.ReadPlc;
using Plc.Domain.S7;

namespace Plc.Application.Behaviors;

/// <summary>
///     筛选者，筛选出符合处理条件的模型
/// </summary>
internal class FilterReadS7PlcPipelineBehavior<TRequest, TResponse>(IS7NetManager netManager)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : PlcEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request.IsBath == true)
        {
            if (request.DeviceName != null)
            {
                request.readBufferInputs =
                    request.readBufferInputs.Where(p => p.HashId == request.DeviceName.GetHashCode());
                request.Key = request.DeviceName;
            }
            else
            {
                request.Key = request.Ip;
            }
        }
        else
        {
            request.readBufferInputs =
                request.readBufferInputs.Where(p => p.HashId == request.DeviceName.GetHashCode());
            request.Key =request.Ip+request.DeviceName;
        }
        return await next();
    }
}