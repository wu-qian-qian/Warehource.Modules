using Common.Application.Caching;
using MediatR;
using Plc.Application.PlcHandler.Read;

namespace Plc.Application.Behaviors.Read;

/// <summary>
///     筛选者，筛选出符合处理条件的模型
/// </summary>
internal class FilterReadS7PlcPipelineBehavior<TRequest, TResponse>(ICacheService cacheService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ReadPlcEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var key = string.Empty;
        ;
        if (request.IsBath)
        {
            if (request.DeviceName != null)
                key = request.Id != null ? request.Id.ToString() : request.DeviceName;
            else
                key = request.Id != null ? request.Id.ToString() : request.Ip;
        }
        else
        {
            key = request.Id.ToString();
        }

        //分布式事件触发会出现无返回值
        if (request.IsApi == false)
        {
            var buffer = await cacheService.GetAsync(key);
            //减少了读取次数保证了
            if (buffer == null)
            {
                var response = await next();
                if (response is byte[] tempbuffer)
                {
                    buffer = tempbuffer;
                    cacheService.SetAsync(key, buffer);
                }
            }

            return default;
        }
        else
        {
            var response = await next();
            if (response is byte[] buffer) cacheService.SetAsync(key, buffer);
            return response;
        }
    }
}