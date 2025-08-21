using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using Microsoft.Extensions.Options;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Contracts.Options;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.WcsTaskAnalysis;

internal class AnalysisCommandHandler(
    IWcsTaskRepository _wcsTaskRepository,
    IExecuteNodeRepository _executeNodeRepository,
    IOptions<StackerMapOptions> mapOptions,
    IUnitOfWork _unitOfWork) : ICommandHandler<AnalysisCommand>
{
    public async Task Handle(AnalysisCommand request, CancellationToken cancellationToken)
    {
        //如果上游系统不维护区域，这个获取路线判断就不能添加区域判断，如入库任务获取到第一节点；然后执行时赋值区域；出移库根据巷道获取到区域
        //区域表示行走路线  一个设备有多个区域，表示多条路线经过该设备

        var options = mapOptions.Value;
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
                if (executeNodeGroup.Any(p => p.Key.TaskType == wcsTask.TaskType
                                              && p.Any(s => s.RegionId == wcsTask.RegionId)))
                {
                    //未提供单区域多路线的解析方式
                    var executeNode = executeNodeGroup
                        .First(p => p.Key.TaskType == wcsTask.TaskType
                                    && p.Any(s => s.RegionId == wcsTask.RegionId));
                    //如果单巷道单设备就直接键值对名称   ，如果单巷道多设备使用键值对设备组
                    if (wcsTask.TaskType == WcsTaskTypeEnum.StockOut || wcsTask.TaskType == WcsTaskTypeEnum.StockMove)
                    {
                        var map = options.StackerMap.FirstOrDefault(p =>
                            p.Tunnle.ToString() == wcsTask.GetLocation.GetTunnel);
                        if (map == null)
                        {
                            Log.Logger.ForCategory(LogCategory.Business)
                                .Information($"{wcsTask.TaskCode}--解析任务出现未知配置巷道--{wcsTask.GetLocation.GetTunnel}");
                            wcsTask.TaskStatus = WcsTaskStatusEnum.Analysited;
                            continue;
                        }

                        wcsTask.TaskExecuteStep.CurentDevice = map.Stacker;
                    }

                    wcsTask.TaskExecuteStep.DeviceType = executeNode?.MinBy(p => p.Index)?.CurrentDeviceType;
                    wcsTask.TaskExecuteStep.PathNodeGroup = executeNode?.Key.PahtNodeGroup;
                    wcsTask.TaskStatus = WcsTaskStatusEnum.Analysited;
                }
            }

            _wcsTaskRepository.Updates(wcsTasks);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}