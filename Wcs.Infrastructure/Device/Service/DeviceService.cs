using Common.Application.Exception;
using Common.Application.Log;
using Common.Shared;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Application.Abstract.Device;
using Wcs.Contracts.Business;
using Wcs.Device.Abstract;
using Wcs.Shared;

namespace Wcs.Infrastructure.Device.Service;

internal class DeviceService : IDeviceService
{
    private readonly IController[] _deviceController;
    private readonly IServiceScopeFactory _scopeFactory;

    public DeviceService(IServiceScopeFactory scopeFactory, params IController[] deviceController)
    {
        _scopeFactory = scopeFactory;
        _deviceController = deviceController;
    }

    /// <summary>
    ///     获取可通行
    /// </summary>
    /// <param name="deviceType"></param>
    /// <returns></returns>
    public Task<int[]?> GetCanExecuteTunnleAsync(DeviceTypeEnum deviceType)
    {
        int[]? tunnels = default;
        var controller = _deviceController
            .First(controller => controller.DeviceType == deviceType);
        if (controller is AbstractStackerTranshipInController targetController)
            tunnels = targetController.GetReCommendTranship();

        return Task.FromResult(tunnels);
    }

    public Task<RecommendTunnle> GetRecommendTunnleAsync(DeviceTypeEnum deviceType, int tunnle)
    {
        var controller = _deviceController
            .First(controller => controller.DeviceType == deviceType);
        RecommendTunnle recommendTunnle = default;
        if (controller is AbstractStackerTranshipInController targetController)
        {
            var transhipInController = targetController.Devices.First(p => p.Config.Tunnle == tunnle);
            recommendTunnle = new RecommendTunnle(transhipInController.Name, transhipInController.Config.PipelinCode);
        }

        return Task.FromResult(recommendTunnle);
    }

    /// <summary>
    ///     设备获取输送
    /// </summary>
    /// <param name="deviceType"></param>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    public Task<string> GetTargetPipelinAsync(DeviceTypeEnum deviceType, string deviceName)
    {
        var controller = _deviceController
            .First(controller => controller.DeviceType == deviceType);
        var targetCode = string.Empty;
        if (controller is AbstractStackerTranshipInController targetController)
        {
            var device = targetController.Devices.First(p => p.Name == deviceName);
            targetCode = device.Config.PipelinCode;
        }

        if (targetCode == string.Empty)
        {
            Log.Logger.ForCategory(LogCategory.Business)
                .Error($"无法解析设备号和任务{deviceType},{deviceName}");
            //这边需要抛出异常，因为不一些设备的对应关系必须要有
            throw new CommonException($"无法解析设备号和任务{deviceType},{deviceName}");
        }

        return Task.FromResult(targetCode);
    }
}