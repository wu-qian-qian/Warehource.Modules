using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Common.Shared;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Device;
using Wcs.Domain.Device;
using Wcs.Domain.Region;

namespace Wcs.Application.Handler.DataBase.Device.AddOrUpdate;

public class AddOrUpdateDeviceCommandHandler(
    IDeviceRepository _deviceRepository,
    IRegionRepository _regionRepository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : ICommandHandler<AddOrUpdateDeviceCommand, Result<DeviceDto>>
{
    public async Task<Result<DeviceDto>> Handle(AddOrUpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        Result<DeviceDto> result = new();
        var regionCodes = request.RegionCode
            .Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

        var regions = _regionRepository.GetQuery()
            .Where(p => regionCodes.Contains(p.Code)).Select(p => p.Code).ToArray();

        request.RegionCode = string.Join(Symbol.Split, regions);
        if (request.Id.HasValue == false)
        {
            var entity = new Domain.Device.Device
            {
                DeviceName = request.DeviceName,
                DeviceType = request.DeviceType.Value,
                Config = request.Config,
                Enable = request.Enable ?? false,
                Description = request.Description,
                GroupName = request.GroupName,
                RegionCode = request.RegionCode
            };
            _deviceRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            result.SetValue(_mapper.Map<DeviceDto>(entity));
        }
        else
        {
            var entity = _deviceRepository.Get(request.Id.Value);
            if (request.DeviceName != null) entity.DeviceName = request.DeviceName;
            if (request.DeviceType != null) entity.DeviceType = request.DeviceType.Value;
            if (request.Config != null) entity.Config = request.Config;
            if (request.Enable != null) entity.Enable = request.Enable.Value;
            if (request.DeviceName != null) entity.Description = request.Description;
            if (request.GroupName != null) entity.GroupName = request.GroupName;
            if (request.RegionCode != null) entity.RegionCode = request.RegionCode;
            _deviceRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            result.SetValue(_mapper.Map<DeviceDto>(entity));
        }

        return result;
    }
}