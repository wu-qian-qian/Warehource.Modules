using AutoMapper;
using Common.Application.MediatR.Message.PageQuery;
using Common.Helper;
using Common.Shared;
using Wcs.Contracts.Respon.Device;
using Wcs.Domain.Device;

namespace Wcs.Application.Handler.DataBase.Device.GetPage;

internal class GetDevicePageCommandHandler(IDeviceRepository _deviceRepository, IMapper _mapper)
    : IPageHandler<GetDevicePageCommand, DeviceDto>
{
    public Task<PageResult<DeviceDto>> Handle(GetDevicePageCommand request, CancellationToken cancellationToken)
    {
        var query = _deviceRepository.GetQueryable()
            .WhereIf(request.DeviceName != null, p => p.DeviceName.Contains(request.DeviceName))
            .WhereIf(request.DeviceType != null, p => p.DeviceType == request.DeviceType)
            .WhereIf(request.Enable.HasValue, p => p.Enable == request.Enable.Value);
        var count = query.Count();
        var data = query.ToPageBySortAsc(request.SkipCount, request.Total, p => p.CreationTime).ToArray();
        var list = _mapper.Map<List<DeviceDto>>(data);
        return Task.FromResult(new PageResult<DeviceDto>(count, list));
    }
}