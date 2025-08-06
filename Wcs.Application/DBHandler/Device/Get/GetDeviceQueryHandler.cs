using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Common.Helper;
using Wcs.Contracts.Respon.Device;
using Wcs.Domain.Device;

namespace Wcs.Application.DBHandler.Device.Get;

public class GetDeviceQueryHandler(IDeviceRepository _deviceRepository, IMapper _mapper)
    : IQueryHandler<GetDeviceQuery, Result<IEnumerable<DeviceDto>>>
{
    public Task<Result<IEnumerable<DeviceDto>>> Handle(GetDeviceQuery request, CancellationToken cancellationToken)
    {
        Result<IEnumerable<DeviceDto>> result = new();
        var data = _deviceRepository.GetQueryable()
            .WhereIf(request.DeviceType != null, x => x.DeviceType == request.DeviceType)
            .WhereIf(request.Enable != null, x => x.Enable == request.Enable)
            .WhereIf(request.DeviceName != null, x => x.DeviceType == request.DeviceType)
            .ToList();
        var source = _mapper.Map<IEnumerable<DeviceDto>>(data);
        result.SetValue(source);
        return Task.FromResult(result);
    }
}