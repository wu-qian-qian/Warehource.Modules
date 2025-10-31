using Common.Application.Caching;
using MassTransit;
using MediatR;
using Plc.CustomEvents;
using Wcs.Contracts.Respon.Plc;
using Wcs.Device.Abstract;
using Wcs.Device.DataBlock;
using Wcs.Device.Helper;
using Wcs.Shared;

namespace Wcs.Application.Handler.DeviceExecute.Behaviors;

/// <summary>
///     统一的PLC读取行为处理管道
///     当从缓存中读取到PLC数据后，才会继续执行业务逻
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// <param name="_bus"></param>
/// <param name="_cacheService"></param>
internal class CanBusinessPipelinBehavior<TRequest, TResponse>(IBus _bus, ICacheService _cacheService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IExecuteDeviceCommand

{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var dbResult =
            await _cacheService.GetAsync<IEnumerable<PlcBuffer>>(request.Device.GetCacheDBKey(), cancellationToken);
        if (dbResult != null)
        {
            BaseDBEntity result = request.Device.DeviceType switch
            {
                DeviceTypeEnum.Stacker =>
                    CreatDBEntity.CreatEntity<StackerDBEntity>(dbResult.ToArray()),
                DeviceTypeEnum.StockPortIn
                    or DeviceTypeEnum.StockPortOut
                    or DeviceTypeEnum.StackerInTranShip
                    or DeviceTypeEnum.StackerOutTranShip =>
                    CreatDBEntity.CreatEntity<PipeLineDBEntity>(dbResult.ToArray()),
                _ => throw new NotImplementedException()
            };
            request.Device.SetDBEntiry(result);
            var res = await next();
            await _cacheService.RemoveAsync(request.Device.GetCacheDBKey());
            return res;
        }
        else
        {
            //优化项 当事件执行中时，不再重复发布读取事件
            await _bus.Publish(new S7ReadPlcDataBlockIntegrationEvent(DateTime.Now)
            {
                DeviceName = request.Device.Name,
                Key = request.Device.GetCacheDBKey(),
                UseMemory = true
            }, cancellationToken);
        }

        return default(TResponse);
    }
}