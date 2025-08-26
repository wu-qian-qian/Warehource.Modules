using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using MediatR;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Application.Handler.Business.CheckExecuteNode;
using Wcs.Application.Handler.Http.ApplyLocation;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.DeviceExecute.StackerTranshipIn;

internal class StackerTranshipInCommandHandler(
    IWcsTaskRepository _wcsTaskRepository,
    IUnitOfWork _unitOfWork,
    ISender sender)
    : ICommandHandler<StackerTranshipInCommand>
{
    public async Task Handle(StackerTranshipInCommand request, CancellationToken cancellationToken)
    {
        var stackerTranshipIn = request.Device;
        if (stackerTranshipIn.CanExecute())
        {
            var wcsTask = _wcsTaskRepository.Get(stackerTranshipIn.DBEntity.RTask);
            if (wcsTask.TaskExecuteStep.CurentDevice == stackerTranshipIn.Name
                || wcsTask.TaskExecuteStep.DeviceType == stackerTranshipIn.DeviceType)
                if (stackerTranshipIn.IsNewStart())
                {
                    if (wcsTask.TaskExecuteStep.CurentDevice
                        == stackerTranshipIn.Name || wcsTask.TaskExecuteStep.DeviceType == stackerTranshipIn.DeviceType)
                    {
                        //事件获取放货位置
                        var result = await sender.Send(new ApplyLocationCommand { TaskCode = wcsTask.TaskCode });
                        if (result.IsSuccess)
                        {
                            var location = result.Value.Split('_');
                            var putLocation =
                                new PutLocation(stackerTranshipIn.Config.Tunnle,
                                    location[0], location[1], location[2], string.Empty);
                            wcsTask.PutLocation = putLocation;
                            //检测
                            var check = await sender.Send(new CheckExecuteNodeCommand
                            {
                                WcsTask = wcsTask,
                                DeviceRegionCode = stackerTranshipIn.RegionCodes,
                                Title = stackerTranshipIn.Config.Tunnle
                            });

                            //因为存在状态追踪
                            if (check.IsSuccess)
                                await _unitOfWork.SaveChangesAsync();
                            else
                                Log.Logger.ForCategory(LogCategory.Business)
                                    .Information($"{stackerTranshipIn.Name}:{check.Message}");
                        }
                        else
                        {
                            Log.Logger.ForCategory(LogCategory.Business)
                                .Information($"{stackerTranshipIn.Name}:{wcsTask.SerialNumber}--申请库位失败");
                        }
                    }
                    else
                    {
                        Log.Logger.ForCategory(LogCategory.Business)
                            .Information($"{stackerTranshipIn.Name}:当前任务不符合设备类型执行失败{wcsTask.SerialNumber}");
                    }
                }
        }
        else
        {
            Log.Logger.ForCategory(LogCategory.Business)
                .Information($"{stackerTranshipIn.Name}：无法执行存在报警或是手动");
        }
    }
}