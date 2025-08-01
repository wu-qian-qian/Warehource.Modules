using Common.Application.Caching;
using MediatR;
using Plc.Application.PlcHandler.Read;
using Plc.Domain.S7;

namespace Plc.Application.Behaviors.Read;

/// <summary>
///     筛选者，筛选出符合处理条件的模型
/// </summary>
internal class FilterReadS7PlcPipelineBehavior<TRequest, TResponse>(ICacheService cacheService)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ReadPlcEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string key = String.Empty;;
        if (request.IsBath)
        {
            if (request.DeviceName != null)
            {
                key = request.Id != null ? request.Id.ToString() : request.DeviceName;
            } 
            else
            {
                key = request.Id != null ? request.Id.ToString() : request.Ip;
            }
        }
        else
        {
            key = request.Id.ToString();
        }
        var response= await next();
        if (response is byte[] buffer)
        {
            cacheService.SetAsync(key, buffer);
        }
        return response;
    }
}