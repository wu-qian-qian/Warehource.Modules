using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Common.Helper;
using Wcs.Contracts.Respon.Device;
using Wcs.Domain.Device;

namespace Wcs.Application.Handler.DB.Device.Get;

public class GetDeviceQueryHandler(IDeviceRepository _deviceRepository, IMapper _mapper)
    : IQueryHandler<GetDeviceQuery, Result<IEnumerable<DeviceDto>>>
{
    public async Task<Result<IEnumerable<DeviceDto>>> Handle(GetDeviceQuery request,
        CancellationToken cancellationToken)
    {
        Result<IEnumerable<DeviceDto>> result = new();
        var data = _deviceRepository.GetQueryable()
            .WhereIf(request.DeviceType != null, x => x.DeviceType == request.DeviceType)
            .WhereIf(request.Enable != null, x => x.Enable == request.Enable)
            .WhereIf(request.DeviceName != null, x => x.DeviceName == request.DeviceName)
            .ToList();
        var source = _mapper.Map<IEnumerable<DeviceDto>>(data);
        result.SetValue(source);
        return result;
    }
}