using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using MassTransit;
using Plc.CustomEvents;
using Serilog;
using Wcs.Contracts.Respon.Plc;
using Wcs.Device.Abstract;
using Wcs.Device.DataBlock;
using Wcs.Device.Helper;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.ReadPlcBlock;

internal class GetPlcDBQueryHandler(
    ICacheService _cacheService,
    IBus _bus) : IQueryHandler<GetPlcDBQuery, BaseDBEntity>
{
    public async Task<BaseDBEntity> Handle(GetPlcDBQuery request, CancellationToken cancellationToken)
    {
        var dbResult = await _cacheService.GetAsync<IEnumerable<PlcBuffer>>(request.Key, cancellationToken);
        BaseDBEntity result = default;
        if (dbResult != null)
        {
            result = request.DeviceType switch
            {
                DeviceTypeEnum.Stacker =>
                    CreatDBEntity.CreatEntity<StackerDBEntity>(dbResult.ToArray(), request.DBEntity),
                DeviceTypeEnum.StockPortIn
                    or DeviceTypeEnum.StockPortOut
                    or DeviceTypeEnum.StackerInTranShip
                    or DeviceTypeEnum.StackerOutTranShip =>
                    CreatDBEntity.CreatEntity<PipeLineDBEntity>(dbResult.ToArray(), request.DBEntity),
                _ => throw new NotImplementedException()
            };
            Log.Logger.ForCategory(LogCategory.Business)
                .Information($"{request.DeviceName}获取PLC数据块数据成功");
        }
        else
        {
            await _bus.Publish(new S7ReadPlcDataBlockIntegrationEvent(DateTime.Now)
            {
                DeviceName = request.DeviceName,
                Key = request.Key,
                UseMemory = request.UseMemory
            }, cancellationToken);
        }

        return result;
    }
}