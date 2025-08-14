using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.ExecuteNode;
using Wcs.Domain.Device;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.Region;

namespace Wcs.Application.Handler.DB.ExecueNode.AddOrUpdate;

public class AddOrUpdateExecuteNodeCommandHandler(
    IExecuteNodeRepository _executeNodeRepository,
    IRegionRepository _regionRepository,
    IDeviceRepository _deviceRepository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : ICommandHandler<AddOrUpdateExecuteNodeCommand, Result<ExecuteNodeDto>>
{
    public async Task<Result<ExecuteNodeDto>> Handle(AddOrUpdateExecuteNodeCommand request,
        CancellationToken cancellationToken)
    {
        var result = new Result<ExecuteNodeDto>();
        var entity = _executeNodeRepository.Get(request.Id);
        var region = _regionRepository.Get(request.RegionCode);
        if (entity == null)
        {
            entity = new ExecuteNodePath
            {
                PahtNodeGroup = request.PahtNodeGroup,
                TaskType = request.TaskType.Value,
                CurrentDeviceType = request.CurrentDeviceType.Value,
                Index = request.Index,
                RegionId = region?.Id
            };
            _executeNodeRepository.Insert([entity]);
            await _unitOfWork.SaveChangesAsync();
            result.SetValue(_mapper.Map<ExecuteNodeDto>(entity));
        }
        else
        {
            entity.PahtNodeGroup = request.PahtNodeGroup;
            entity.TaskType = request.TaskType.Value;
            entity.CurrentDeviceType = request.CurrentDeviceType.Value;
            _executeNodeRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            result.SetValue(_mapper.Map<ExecuteNodeDto>(entity));
        }

        return result;
    }
}