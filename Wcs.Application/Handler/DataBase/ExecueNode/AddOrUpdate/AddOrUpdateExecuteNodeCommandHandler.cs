using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.ExecuteNode;
using Wcs.Domain.Device;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.Region;

namespace Wcs.Application.Handler.DataBase.ExecueNode.AddOrUpdate;

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
        ExecuteNodePath entity;
        if (request.Id.HasValue == false)
        {
            byte index;
            var region = _regionRepository.Get(request.RegionCode);
            if (_executeNodeRepository.GetQuerys().Any(p => p.PahtNodeGroup == request.PahtNodeGroup))
            {
                index = _executeNodeRepository.GetQuerys()
                    .Where(p => p.PahtNodeGroup == request.PahtNodeGroup)
                    .OrderBy(P => P.CreationTime)
                    .Select(p => p.Index).First();
                index += 1;
            }
            else
            {
                index = 1;
            }

            entity = new ExecuteNodePath
            {
                PahtNodeGroup = request.PahtNodeGroup,
                TaskType = request.TaskType.Value,
                CurrentDeviceType = request.CurrentDeviceType.Value,
                Index = index,
                RegionId = region?.Id
            };
            _executeNodeRepository.Insert([entity]);
        }
        else
        {
            entity = _executeNodeRepository.Get(request.Id.Value);
            entity.PahtNodeGroup = request.PahtNodeGroup;
            entity.TaskType = request.TaskType.Value;
            entity.CurrentDeviceType = request.CurrentDeviceType.Value;
            entity.Index = request.Index ?? entity.Index;
        }

        await _unitOfWork.SaveChangesAsync();
        result.SetValue(_mapper.Map<ExecuteNodeDto>(entity));
        return result;
    }
}