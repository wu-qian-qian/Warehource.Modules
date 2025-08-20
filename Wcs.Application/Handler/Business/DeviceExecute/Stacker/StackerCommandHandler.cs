using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using MassTransit;
using MediatR;
using Serilog;
using Wcs.Application.Handler.Business.FilterStackerTask;
using Wcs.Application.Handler.Http.Complate;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.DeviceExecute.Stacker;

/// <summary>
///     堆垛机业务处理事件
/// </summary>
/// <param name="sender"></param>
internal class StackerCommandHandler(ISender sender, IPublishEndpoint _publishEndpoint, ICacheService _cacheService)
    : ICommandHandler<StackerCommand>
{
    public async Task Handle(StackerCommand request, CancellationToken cancellationToken)
    {
        var stacker = request.Stacker;
        //判断是否可执行
        if (stacker.CanExecute())
        {
            //获取当前堆垛机执行的任务
            var wcsTask = await _cacheService.GetAsync<WcsTask>(stacker.Config.TaskKey, cancellationToken);
            //当前堆垛机无执行任务则从数据库中查找执行
            if (wcsTask == null)
            {
                //获取该设备所有任务
                var wcsTasks = await sender.Send(new GetFilterStackerQuery
                        { DeviceName = stacker.Name, DeviceType = stacker.DeviceType }
                    , cancellationToken);
                //获取到执行任务
                wcsTask = await sender.Send(new FilterStackerCommand
                {
                    IsTranShipPoint = stacker.IsTranShipPoint(),
                    WcsTasks = wcsTasks
                });
                if (wcsTask != null)
                {
                    await _cacheService.SetAsync(stacker.Config.TaskKey, wcsTask,
                        cancellationToken: cancellationToken);
                    Log.Logger.ForCategory(LogCategory.Business)
                        .Information($"{stacker.Name}-获取执行任务-{wcsTask.SerialNumber}");
                }
            }
            else
            {
                if (stacker.IsComplate())
                {
                    switch (wcsTask.TaskType)
                    {
                        case WcsTaskTypeEnum.StockIn:
                        case WcsTaskTypeEnum.StockMove:
                            var result = await sender.Send(new ComplateCommand(), cancellationToken);
                            break;
                        case WcsTaskTypeEnum.StockOut:
                            // //sender 通知其他设备服务
                            break;
                    }

                    await _cacheService.RemoveAsync(stacker.Config.TaskKey);
                    Log.Logger.ForCategory(LogCategory.Business)
                        .Information($"{stacker.Name}-堆垛机执行任务完成-{wcsTask.SerialNumber}");
                }
                else
                {
                    //写入数据
                    if (stacker.IsNewStart())
                        if (wcsTask.TaskExecuteStep.TaskExecuteStepType == TaskExecuteStepTypeEnum.ToBeSend)
                        {
                            var dic = new Dictionary<string, string>();
                            dic.Add(stacker.CreatWriteExpression(p => p.RTask), wcsTask.SerialNumber.ToString());
                            //判断是否写入成功  更新到数据库
                            //if(stacker.DBEntity.RTask.Equals(wcsTask.SerialNumber.ToString()))

                            //可以使用Saga进行状态机执行  ，发送第一次wcsTask.TaskExecuteStep.IsSend=true更新到缓存，执行状态机，若成功不进行处理，失败则改为false
                        }
                }
            }
        }
        else
        {
            Log.Logger.ForCategory(LogCategory.Business)
                .Information($"{stacker.Name}--堆垛机连接异常无法执行任务");
        }
    }
}