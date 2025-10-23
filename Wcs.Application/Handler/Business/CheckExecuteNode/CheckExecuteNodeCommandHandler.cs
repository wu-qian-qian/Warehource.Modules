using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Handler.Business.GetNetNode;
using Wcs.Application.Handler.Http.Complate;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.Region;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.CheckExecuteNode;

/// <summary>
///     最好修改成领域事件
/// </summary>
/// <param name="_sender"></param>
/// <param name="_executeNodeRepository"></param>
/// <param name="_regionRepository"></param>
/// <param name="scopeFactory"></param>
public class CheckExecuteNodeCommandHandler(
    ISender _sender,
    IExecuteNodeRepository _executeNodeRepository,
    IRegionRepository _regionRepository,
    IServiceScopeFactory scopeFactory
) : ICommandHandler<CheckExecuteNodeCommand, Result<bool>>
{
    private static readonly SemaphoreSlim _semaphore = new(1, 1);

    //TODO 路径可以考虑使用缓存减少数据库访问
    public async Task<Result<bool>> Handle(CheckExecuteNodeCommand request, CancellationToken cancellationToken)
    {
        Result<bool> result = new();
        var wcsTask = request.WcsTask;
        //任务区域
        var region = _regionRepository.Get(wcsTask.RegionId.Value);
        if (region != null)
        {
            //设备的区域组保函该区域
            if (request.DeviceRegionCode.Contains(region.Code))
            {
                var executeNodePath = _executeNodeRepository.GetQuerys()
                    .Where(p => p.PahtNodeGroup == wcsTask.TaskExecuteStep.PathNodeGroup).ToArray();
                if (executeNodePath.All(p => p.Region.Id == wcsTask.RegionId))
                {
                    var index = executeNodePath.First(p => p.CurrentDeviceType == wcsTask.TaskExecuteStep.DeviceType)
                        .Index;
                    if (index == 1)
                        try
                        {
                            await _semaphore.WaitAsync(cancellationToken);
                            if (region.MaxNum <= region.CurrentNum)
                            {
                                result.SetMessage("限流处理");
                                return result;
                            }
                            else
                            {
                                region.CurrentNum += 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            result.SetMessage(ex.Message);
                        }
                        finally
                        {
                            _semaphore.Release();
                        }

                    var nextType = index++;
                    var nextNode = executeNodePath.FirstOrDefault(p => p.Index == nextType);
                    if (nextNode != null)
                    {
                        wcsTask.TaskExecuteStep.DeviceType = nextNode.CurrentDeviceType;
                        //获取下一节点所执行的设备
                        if (request.IsGetNextNode)
                        {
                            var deviceName =
                                await _sender.Send(new GetNextNodeCommand
                                {
                                    DeviceType = wcsTask.TaskExecuteStep.DeviceType.Value,
                                    Filter = request.Title,
                                    RegionCode = region.Code
                                }, cancellationToken);
                            if (deviceName != null || deviceName != string.Empty)
                            {
                                wcsTask.TaskExecuteStep.CurentDevice = deviceName;
                                result.SetValue(true);
                            }
                            else
                            {
                                result.SetMessage("无法获取到下一节点执行设备");
                            }
                        }
                        else
                        {
                            result.SetValue(true);
                        }
                    }
                    else
                    {
                        result.SetValue(false);
                        try
                        {
                            await _semaphore.WaitAsync(cancellationToken);
                            region.CurrentNum -= 1;
                        }
                        catch (Exception ex)
                        {
                            result.SetMessage(ex.Message);
                        }
                        finally
                        {
                            _semaphore.Release();
                        }

                        wcsTask.TaskStatus = WcsTaskStatusEnum.Completed;
                        await _sender.Send(new ComplateCommand
                        {
                            WcsTask = request.WcsTask
                        });
                    }
                }
                else
                {
                    result.SetMessage("任务执行的区域与路径区域不符合");
                }
            }
            else
            {
                result.SetMessage("区域信息错误无法执行");
            }
        }
        else
        {
            result.SetMessage("区域信息未获取到");
        }

        return result;
    }
}