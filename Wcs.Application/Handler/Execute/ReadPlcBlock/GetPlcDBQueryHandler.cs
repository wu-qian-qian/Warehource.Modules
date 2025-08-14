using Common.Application.Caching;
using Common.Application.Event;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using Plc.CustomEvents;
using Serilog;
using Wcs.Contracts.Respon.Plc;
using Wcs.Device.DeviceDB;
using Wcs.Domain.Plc;
using Wcs.Shared;

namespace Wcs.Application.Handler.Execute.ReadPlcBlock;

internal class GetPlcDBQueryHandler(
    ICacheService _cacheService,
    IPlcMapRepository _plcMapRepository,
    IMassTransitEventBus _bus) : IQueryHandler<GetPlcDBQuery, BaseEntity>
{
    public async Task<BaseEntity> Handle(GetPlcDBQuery request, CancellationToken cancellationToken)
    {
        var dbResult = await _cacheService.GetAsync<IEnumerable<PlcBuffer>>(request.DeviceName, cancellationToken);
        BaseEntity result = default;
        if (dbResult != null)
        {
            result = request.DeviceType switch
            {
                DeviceTypeEnum.Stacker => StackerDBEntity.CreatDBEntity(dbResult.ToArray()),
                DeviceTypeEnum.StockPortIn => throw new NotImplementedException(),
                DeviceTypeEnum.StockPortOut => throw new NotImplementedException(),
                DeviceTypeEnum.StackerInTranShip => throw new NotImplementedException(),
                DeviceTypeEnum.StackerOutTranShip => throw new NotImplementedException(),
                _ => throw new NotImplementedException()
            };
            Log.Logger.ForCategory(LogCategory.Business)
                .Information($"{request.DeviceName}获取PLC数据块数据成功");
        }
        else
        {
            await _bus.PublishAsync(new S7ReadPlcDataBlockEvent(DateTime.Now)
            {
                DeviceName = request.DeviceName,
                UseMemory = false
            }, cancellationToken);
        }

        return result;
    }
}