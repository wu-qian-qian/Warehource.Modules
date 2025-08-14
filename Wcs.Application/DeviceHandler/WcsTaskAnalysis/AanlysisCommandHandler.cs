using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.DeviceHandler.WcsTaskAnalysis;

internal class AanlysisCommandHandler(
    IWcsTaskRepository _wcsTaskRepository,
    IExecuteNodeRepository _executeNodeRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<AnalysisCommandEvent>
{
    public async Task Handle(AnalysisCommandEvent request, CancellationToken cancellationToken)
    {
        var wcsTasks = _wcsTaskRepository
            .GetWcsTaskQuerys()
            .Where(p => p.TaskStatus == WcsTaskStatusEnum.Created)
            .ToArray();
        if (wcsTasks.Length != 0)
        {
            //获取到启用的路线
            var executeNodeGroup = _executeNodeRepository
                .GetQuerys()
                .GroupBy(p => new { p.PahtNodeGroup, p.TaskType })
                .Where(p => p.All(x => x.Enable)).ToArray();
            for (var i = 0; i < wcsTasks.Length; i++)
            {
                var wcsTask = wcsTasks[i];
                if (executeNodeGroup.Any(p => p.Key.TaskType == wcsTask.TaskType))
                {
                    var executeNode = executeNodeGroup
                        .Where(p => p.Key.TaskType == wcsTask.TaskType).ToArray();
                    //挑选路径
                    if (executeNode.Count() == 1)
                    {
                        //当前只有一条路
                        wcsTask.TaskExecuteStep.DeviceType = executeNode[0]?.MinBy(p => p.Index)?.CurrentDeviceType;
                        wcsTask.TaskExecuteStep.PathNodeGroup = executeNode[0]?.Key.PahtNodeGroup;
                        wcsTask.TaskStatus = WcsTaskStatusEnum.Analysited;
                    }
                    else if (executeNode.Count() > 1)
                    {
                        //多条需要按照区域进行挑选、路线数量
                        Log.Logger.ForCategory(LogCategory.Business)
                            .Information("无对多条路径解析方案");
                    }
                    else
                    {
                        Log.Logger.ForCategory(LogCategory.Business)
                            .Information("任务执行路径解析失败");
                    }
                }
            }

            _wcsTaskRepository.Updates(wcsTasks);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}