using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Domain.Region;
using Wcs.Domain.Task;
using Wcs.Domain.TaskExecuteStep;
using Wcs.Shared;

namespace Wcs.Application.DBHandler.WcsTask.Insert;

public class InsertWcsTaskHandler(
    IWcsTaskRepository _wcsTaskRepository,
    IRegionRepository _regionRepository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : ICommandHandler<InsertWcsTaskEvent, WcsTaskDto>
{
    public async Task<WcsTaskDto> Handle(InsertWcsTaskEvent request, CancellationToken cancellationToken)
    {
        //TODO 根据规则匹配区域
        var wcsTask = new Domain.Task.WcsTask
        {
            TaskCode = request.TaskCode,
            CreatorSystemType = request.CreatorSystemType,
            TaskStatus = WcsTaskStatusEnum.Created,
            TaskType = request.TaskType,
            Container = request.Container,
            PutLocation = new PutLocation(request.PutTunnel.ToString()
                , request.PutFloor.ToString(), request.PutRow.ToString()
                , request.PutColumn.ToString(), request.PutDepth.ToString()),
            GetLocation = new GetLocation(request.GetTunnel.ToString()
                , request.GetFloor.ToString(), request.GetRow.ToString()
                , request.GetColumn.ToString(), request.GetDepth.ToString()),
            Description = request.Description,
            TaskExecuteStep = new TaskExecuteStep
            {
                Description = "任务创建"
            }
        };
        _wcsTaskRepository.Insert([wcsTask]);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<WcsTaskDto>(wcsTask);
    }
}