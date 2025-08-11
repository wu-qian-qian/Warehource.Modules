using Common.Application.Caching;
using Common.Application.Event;
using Common.Application.MediatR.Message;
using Plc.CustomEvents;
using Wcs.Contracts.Respon.Plc;
using Wcs.Device;
using Wcs.Domain.Plc;

namespace Wcs.Application.ExecuteDevice.ReadPlcBlock;

internal class GetPlcDBQueryHandler(
    ICacheService _cacheService,
    IPlcMapRepository _plcMapRepository,
    IMassTransitEventBus _bus) : IQueryHandler<GetPlcDBQuery, BaseEntity>
{
    public async Task<BaseEntity> Handle(GetPlcDBQuery request, CancellationToken cancellationToken)
    {
        var dbResult = await _cacheService.GetAsync<IEnumerable<PlcBuffer>>(request.DeviceName);
        if (dbResult == null)
        {
            var plcMap = _plcMapRepository.GetPlcMapOfDeviceName(request.DeviceName);
            foreach (var item in dbResult)
            {
            }
        }
        else
        {
            await _bus.PublishAsync(new S7ReadPlcDataBlockEvent(DateTime.Now)
            {
                DeviceName = request.DeviceName,
                UseMemory = false
            });
        }

        return null;
    }
}