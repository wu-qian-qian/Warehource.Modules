using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Domain.Region;
using Wcs.Domain.Task;
using Wcs.Domain.TaskExecuteStep;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.WcsTask.AddOrUpdate;

public class AddOrUpdateWcsTaskCommandHandler(
    IWcsTaskRepository _wcsTaskRepository,
    IRegionRepository _regionRepository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : ICommandHandler<AddOrUpdateWcsTaskCommand, Result<WcsTaskDto>>
{
    public async Task<Result<WcsTaskDto>> Handle(AddOrUpdateWcsTaskCommand request, CancellationToken cancellationToken)
    {
        Result<WcsTaskDto> result = new();
        var wcsTask = _wcsTaskRepository.Get(request.Id);
        //这边是上游传输，需要注意如果上游是可以双区域任意入库则需要进一步细化如获取某一个任务少量的区域
        var region = _regionRepository.Get(request.RegionCode);
        if (wcsTask == null)
        {
            wcsTask = new Domain.Task.WcsTask
            {
                Level = request.Level,
                IsEnforce = request.IsEnforce,
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
                StockInPosition = request.StockInPosition,
                StockOutPosition = request.StockOutPosition,
                RegionId = region?.Id,
                TaskExecuteStep = new TaskExecuteStep
                {
                    Description = "任务创建"
                }
            };
            _wcsTaskRepository.Insert([wcsTask]);
        }
        else
        {
            if (wcsTask.TaskCode == request.TaskCode)
            {
                wcsTask.Level = request.Level;
                wcsTask.IsEnforce = request.IsEnforce;
                var getTunnel = request.GetTunnel != null
                    ? request.GetTunnel.ToString()
                    : wcsTask.GetLocation.GetTunnel;
                var getRow = request.GetRow != null ? request.GetRow.ToString() : wcsTask.GetLocation.GetRow;
                var getColum = request.GetColumn != null ? request.GetColumn.ToString() : wcsTask.GetLocation.GetColumn;
                var getFloor = request.GetFloor != null ? request.GetFloor.ToString() : wcsTask.GetLocation.GetFloor;

                var putRow = request.PutRow != null ? request.PutRow.ToString() : wcsTask.PutLocation.PutRow;
                var putColum = request.PutColumn != null ? request.PutColumn.ToString() : wcsTask.PutLocation.PutColumn;
                var putFloor = request.PutFloor != null ? request.PutFloor.ToString() : wcsTask.PutLocation.PutFloor;
                var putTunnel = request.PutTunnel != null
                    ? request.PutTunnel.ToString()
                    : wcsTask.PutLocation.PutTunnel;

                var getLocation = new GetLocation(getTunnel, getFloor, getRow, getColum, wcsTask.GetLocation.GetDepth);
                var putLocation = new PutLocation(putTunnel, putFloor, putRow, putColum, wcsTask.PutLocation.PutRow);

                wcsTask.GetLocation = getLocation;
                wcsTask.PutLocation = putLocation;
            }
            else
            {
                result.SetMessage("任务信息错误无法更新任务数据");
            }
        }

        await _unitOfWork.SaveChangesAsync();
        result.SetValue(_mapper.Map<WcsTaskDto>(wcsTask));
        return result;
    }
}