using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.ExecuteNode;
using Wcs.Domain.Device;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.Region;

namespace Wcs.Application.DBHandler.ExecueNode.AddOrUpdate;

public class AddOrUpdateExecuteNodeHandler(
    IExecuteNodeRepository _executeNodeRepository,
    IRegionRepository _regionRepository,
    IDeviceRepository _deviceRepository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : ICommandHandler<AddOrUpdateExecuteNodeEvent, Result<ExecuteNodeDto>>
{
    public async Task<Result<ExecuteNodeDto>> Handle(AddOrUpdateExecuteNodeEvent request,
        CancellationToken cancellationToken)
    {
        var result = new Result<ExecuteNodeDto>();
        var region = _regionRepository.Get(request.RegionCode);
        var device = _deviceRepository.Get(request.CurrentDeviceName);
        var entity = _executeNodeRepository.Get(request.Id);
        if (entity == null)
        {
            if (region != null && device != null)
            {
                entity = new ExecuteNodePath
                {
                    PahtNodeGroup = request.PahtNodeGroup,
                    CurrentDeviceName = request.CurrentDeviceName,
                    TaskType = request.TaskType,
                    CurrentDeviceType = device.DeviceType,
                    Region = region,
                    RegionId = region.Id,
                    NextDeviceName = request.NextDeviceName
                };
                _executeNodeRepository.Insert([entity]);
                await _unitOfWork.SaveChangesAsync();
                result.SetValue(_mapper.Map<ExecuteNodeDto>(entity));
            }
            else
            {
                result.SetMessage("无区域和设备编码");
            }
        }
        else
        {
            if (region != null && device != null)
            {
                result.SetMessage("无区域和设备编码");
            }
            else
            {
                entity.PahtNodeGroup = request.PahtNodeGroup;
                entity.CurrentDeviceName = request.CurrentDeviceName;
                entity.TaskType = request.TaskType;
                entity.CurrentDeviceType = device.DeviceType;
                entity.Region = region;
                entity.RegionId = region.Id;
                entity.NextDeviceName = request.NextDeviceName;
                _executeNodeRepository.Update(entity);
                await _unitOfWork.SaveChangesAsync();
                result.SetValue(_mapper.Map<ExecuteNodeDto>(entity));
            }
        }

        return result;
    }
}