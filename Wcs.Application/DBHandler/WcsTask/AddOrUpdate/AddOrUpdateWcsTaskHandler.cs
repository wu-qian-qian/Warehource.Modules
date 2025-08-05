using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Domain.Region;
using Wcs.Domain.Task;
using Wcs.Domain.TaskExecuteStep;
using Wcs.Shared;

namespace Wcs.Application.DBHandler.WcsTask.Insert;

public class AddOrUpdateWcsTaskHandler(
    IWcsTaskRepository _wcsTaskRepository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : ICommandHandler<AddOrUpdateWcsTaskEvent, WcsTaskDto>
{
    public async Task<WcsTaskDto> Handle(AddOrUpdateWcsTaskEvent request, CancellationToken cancellationToken)
    {
        //TODO 根据规则匹配区域
        var wcsTask = _wcsTaskRepository.Get(request.SerialNumber.Value);
        if (wcsTask == null&&request.SerialNumber==null)
        {
            wcsTask = new Domain.Task.WcsTask
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
        }
        else
        {
            var getTunnel = request.GetTunnel != null ? request.GetTunnel.ToString() : wcsTask.GetLocation.GetTunnel;
            var getRow= request.GetRow!=null?request.GetRow.ToString() : wcsTask.GetLocation.GetRow;
            var getColum = request.GetColumn != null ? request.GetColumn.ToString() : wcsTask.GetLocation.GetColumn;
            var getFloor = request.GetFloor != null ? request.GetFloor.ToString() : wcsTask.GetLocation.GetFloor;

            var putRow = request.PutRow != null ? request.PutRow.ToString() : wcsTask.PutLocation.PutRow;
            var putColum = request.PutColumn != null ? request.PutColumn.ToString() : wcsTask.PutLocation.PutColumn;
            var putFloor = request.PutFloor != null ? request.PutFloor.ToString() : wcsTask.PutLocation.PutFloor;
            var putTunnel = request.PutTunnel != null ? request.PutTunnel.ToString() : wcsTask.PutLocation.PutTunnel;

            GetLocation getLocation=new GetLocation(getTunnel, getFloor,getRow,getColum,wcsTask.GetLocation.GetDepth);
            PutLocation putLocation = new PutLocation(putTunnel, putFloor, putRow, putColum, wcsTask.PutLocation.PutRow);

            wcsTask.GetLocation = getLocation;
            wcsTask.PutLocation = putLocation;
        }
      
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<WcsTaskDto>(wcsTask);
    }
}