using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Common.Shared;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Device;
using Wcs.Domain.Device;
using Wcs.Domain.Region;

namespace Wcs.Application.DBHandler.Device.AddOrUpdate;

public class AddOrUpdateDeviceHandler(
    IDeviceRepository _deviceRepository,
    IRegionRepository _regionRepository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : ICommandHandler<AddOrUpdateDeviceEvent, Result<DeviceDto>>
{
    public async Task<Result<DeviceDto>> Handle(AddOrUpdateDeviceEvent request, CancellationToken cancellationToken)
    {
        Result<DeviceDto> result = new();
        var entity = _deviceRepository.Get(request.Id);

        var regionCodes = request.RegionCode
            .Split(request.RegionCode, Symbol.Split[0], StringSplitOptions.RemoveEmptyEntries).ToList();

        var regions = _regionRepository.GetQuery()
            .Where(p => regionCodes.Contains(p.Code)).Select(p => p.Code).ToArray();

        request.RegionCode = string.Join(Symbol.Split, regions);
        if (entity == null)
        {
            entity = new Domain.Device.Device
            {
                DeviceName = request.DeviceName,
                DeviceType = request.DeviceType.Value,
                Config = request.Config,
                Enable = request.Enable.Value,
                Description = request.Description,
                GroupName = request.GroupName
            };
            _deviceRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            result.SetValue(_mapper.Map<DeviceDto>(entity));
        }
        else
        {
            entity.DeviceName = request.DeviceName;
            entity.DeviceType = request.DeviceType.Value;
            entity.Config = request.Config;
            entity.Enable = request.Enable.Value;
            entity.Description = request.Description;
            entity.GroupName = request.GroupName;
            _deviceRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            result.SetValue(_mapper.Map<DeviceDto>(entity));
        }

        return result;
    }
}