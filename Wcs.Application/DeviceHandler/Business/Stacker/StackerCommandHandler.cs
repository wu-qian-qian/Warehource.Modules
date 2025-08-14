using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using MediatR;
using Serilog;
using Wcs.Application.DeviceHandler.GetWcsTask;
using Wcs.Application.DeviceHandler.ReadPlcBlock;
using Wcs.Application.Http.Complate;
using Wcs.Device.Device.BaseExecute;
using Wcs.Device.DeviceDB;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.DeviceHandler.Business.Stacker;

/// <summary>
///     堆垛机业务处理事件
/// </summary>
/// <param name="sender"></param>
internal class StackerCommandHandler(ISender sender, ICacheService _cacheService) : ICommandHandler<StackerCommandEvent>
{
    public async Task Handle(StackerCommandEvent request, CancellationToken cancellationToken)
    {
        var stacker = request.Stacker;
        var stackerDBEntity = (StackerDBEntity)
            await sender.Send(new GetPlcDBQuery { DeviceName = stacker.Name, DeviceType = DeviceTypeEnum.Stacker },
                cancellationToken);
        if (stackerDBEntity != null)
        {
            stacker.SetDBEntity(stackerDBEntity);
            //判断是否可执行
            if (stacker.CanExecute())
            {
                var key = $"{stacker.Name}Task";
                await Execute(key, sender, stacker, _cacheService, cancellationToken);
            }
            else
            {
                Log.Logger.ForCategory(LogCategory.Business)
                    .Information($"{stacker.Name}--堆垛机连接异常无法执行任务");
            }
        }
    }

    private async Task Execute(string key, ISender sender, AbstractStacker stacker, ICacheService cacheService,
        CancellationToken cancellationToken = default)
    {
        //获取当前堆垛机执行的任务
        var wcsTask = await cacheService.GetAsync<WcsTask>(key);
        //当前堆垛机无执行任务则从数据库中查找执行
        if (wcsTask == null)
        {
            var wcsTasks = await sender.Send(new GetWcsTaskEvent
            {
                DeviceName = stacker.Name,
                DeviceType = DeviceTypeEnum.Stacker,
                IsTranShipPoint = stacker.IsTranShipPoint()
            });
            if (wcsTasks.Any()) await _cacheService.SetAsync(key, wcsTasks.First());
        }
        else
        {
            if (stacker.IsComplate())
            {
                switch (wcsTask.TaskType)
                {
                    case WcsTaskTypeEnum.StockIn:
                    case WcsTaskTypeEnum.StockMove:
                        var result = await sender.Send(new ComplateCommandEvent());
                        await cacheService.RemoveAsync(key);
                        //sender 通知任务完成
                        break;
                    case WcsTaskTypeEnum.StockOut:
                        // //sender 通知其他设备服务
                        break;
                }
            }
            else
            {
                //写入数据
                if (stacker.IsNewStart())
                    if (wcsTask.TaskExecuteStep.IsSend == false)
                    {
                        var dic = new Dictionary<string, string>();
                        dic.Add(stacker.CreatWriteExpression(p => p.RTask), "1");
                        //判断是否写入成功  更新到数据库
                    }
            }
        }
    }
}