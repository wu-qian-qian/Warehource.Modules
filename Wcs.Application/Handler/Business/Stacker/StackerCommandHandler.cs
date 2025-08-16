using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using MediatR;
using Serilog;
using Wcs.Application.Handler.Execute.GetWcsTask;
using Wcs.Application.Handler.Execute.ReadPlcBlock;
using Wcs.Application.Handler.Http.Complate;
using Wcs.Device.DataBlock;
using Wcs.Device.Device.Stacker;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.Stacker;

/// <summary>
///     堆垛机业务处理事件
/// </summary>
/// <param name="sender"></param>
internal class StackerCommandHandler(ISender sender, ICacheService _cacheService) : ICommandHandler<StackerCommand>
{
    public async Task Handle(StackerCommand request, CancellationToken cancellationToken)
    {
        var stacker = request.Stacker;
        var stackerDBEntity = (StackerDBEntity)
            await sender.Send(
                new GetPlcDBQuery
                {
                    DeviceName = stacker.Name,
                    Key = stacker.Config.DBKey,
                    DeviceType = DeviceTypeEnum.Stacker,
                    DBEntity = stacker.DBEntity
                },
                cancellationToken);
        if (stackerDBEntity != null)
        {
            stacker.SetDBEntity(stackerDBEntity);
            //判断是否可执行
            if (stacker.CanExecute())
                await Execute(sender, stacker, _cacheService, cancellationToken);
            else
                Log.Logger.ForCategory(LogCategory.Business)
                    .Information($"{stacker.Name}--堆垛机连接异常无法执行任务");
        }
    }

    private async Task Execute(ISender sender, AbstractStacker stacker, ICacheService cacheService,
        CancellationToken cancellationToken = default)
    {
        //获取当前堆垛机执行的任务
        var wcsTask = await cacheService.GetAsync<WcsTask>(stacker.Config.TaskKey, cancellationToken);
        //当前堆垛机无执行任务则从数据库中查找执行
        if (wcsTask == null)
        {
            var wcsTasks = await sender.Send(new GetWcsTaskQuery
            {
                DeviceName = stacker.Name,
                DeviceType = DeviceTypeEnum.Stacker,
                IsTranShipPoint = stacker.IsTranShipPoint()
            });
            if (wcsTasks.Any())
            {
                await _cacheService.SetAsync(stacker.Config.TaskKey, wcsTasks.First(),
                    cancellationToken: cancellationToken);
                Log.Logger.ForCategory(LogCategory.Business)
                    .Information($"{stacker.Name}-获取执行任务-{wcsTasks.First().SerialNumber}");
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
                        await cacheService.RemoveAsync(stacker.Config.TaskKey);
                        //sender 通知任务完成
                        break;
                    case WcsTaskTypeEnum.StockOut:
                        // //sender 通知其他设备服务
                        break;
                }

                Log.Logger.ForCategory(LogCategory.Business)
                    .Information($"{stacker.Name}-堆垛机执行任务完成-{wcsTask.SerialNumber}");
            }
            else
            {
                //写入数据
                if (stacker.IsNewStart())
                    if (wcsTask.TaskExecuteStep.IsSend == false)
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
}