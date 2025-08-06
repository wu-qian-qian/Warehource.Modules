using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Device;
using Wcs.Domain.Device;

namespace Wcs.Application.DBHandler.Device.AddOrUpdate;

public class AddOrUpdateDeviceHandler(
    IDeviceRepository _deviceRepository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : ICommandHandler<AddOrUpdateDeviceEvent, Result<DeviceDto>>
{
    public async Task<Result<DeviceDto>> Handle(AddOrUpdateDeviceEvent request, CancellationToken cancellationToken)
    {
        Result<DeviceDto> result = new();
        var entity = _deviceRepository.Get(request.Id);
        if (entity == null)
        {
            entity = new Domain.Device.Device
            {
                DeviceName = request.DeviceName,
                DeviceType = request.DeviceType.Value,
                Config = request.Config,
                Enable = request.Enable.Value,
                Description = request.Description
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
            _deviceRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            result.SetValue(_mapper.Map<DeviceDto>(entity));
        }

        return result;
    }
}