using Common.Application.Exception;
using Common.Application.Log;
using Common.Shared;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Wcs.Application.Abstract;
using Wcs.Application.Abstract.Device;
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
    public Task<string[]?> GetCanExecuteTunnleAsync(DeviceTypeEnum deviceType)
    {
        string[]? tunnels = default;
        var controller = _deviceController
            .First(controller => controller.DeviceType == deviceType);
        if (controller is AbstractStackerTranshipInController targetController)
            tunnels = targetController.GetReCommendTranship();

        return Task.FromResult(tunnels);
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

    public Task<string> GetTranshipPositionAsync(DeviceTypeEnum deviceType, string tunnle)
    {
        var controller = _deviceController
            .First(controller => controller.DeviceType == deviceType);
        var location = string.Empty;
        if (controller is AbstractStackerTranshipInController targetInController)
        {
            var tarController = targetInController.Devices
                .First(p => p.Config.Tunnle == tunnle);
            location =
                $"{tarController.Config.Tunnle}_{tarController.Config.Floor}_{tarController.Config.Row}_{tarController.Config.Column}";
        }
        else if (controller is AbstractStackerTranshipInController targetOutController)
        {
            var tarController = targetOutController.Devices
                .First(p => p.Config.Tunnle == tunnle);
            location =
                $"{tarController.Config.Tunnle}_{tarController.Config.Floor}_{tarController.Config.Row}_{tarController.Config.Column}";
        }

        return Task.FromResult(location);
    }

    /// <summary>
    ///     根据条件获取设备
    /// </summary>
    /// <param name="deviceType"></param>
    /// <param name="tunnle"></param>
    /// <returns></returns>
    public Task<string> GetDeviceNameAsync(DeviceTypeEnum deviceType, string title)
    {
        var controller = _deviceController
            .First(controller => controller.DeviceType == deviceType);
        var deviceName = string.Empty;
        if (controller is AbstractStackerTranshipInController targetInController)
        {
            var tarController = targetInController.Devices
                .First(p => p.Config.Tunnle == title);
            deviceName = tarController.Name;
        }
        else if (controller is AbstractStackerTranshipInController targetOutController)
        {
            var tarController = targetOutController.Devices
                .First(p => p.Config.Tunnle == title);
            deviceName = tarController.Name;
        }
        else if (controller is AbstractStackerController stackerController)
        {
            var tarController = stackerController.Devices
                .First(p => p.Config.Tunnle == title);
            deviceName = tarController.Name;
        }
        else if (controller is AbstractStockOutPortController stockPortController)
        {
            var tarController = stockPortController.Devices
                .First(p => p.Config.PipeLineCode == title);
            deviceName = tarController.Name;
        }

        if (deviceName == string.Empty)
            throw new AggregateException($"{deviceType}未获取到设备信息，请检查配置加载,{title}");
        return Task.FromResult(deviceName);
    }
}