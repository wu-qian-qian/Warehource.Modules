using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Abstract;
using Wcs.Application.DeviceController.Stacker;
using Wcs.Application.DeviceController.StockPort;
using Wcs.Application.DeviceController.Tranship;
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
    public Task<string[]?> GetCanExecuteTunnleAsync(DeviceTypeEnum deviceType, string regoinCode)
    {
        string[]? tunnels = default;
        var controller = _deviceController
            .First(controller => controller.DeviceType == deviceType);
        if (controller is AbstractStackerTranshipController targetController)
            tunnels = targetController.GetReCommendTranship(regoinCode);

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
        if (controller is AbstractStackerTranshipController targetController)
            targetCode = targetController.GetCurrentPipline(deviceName);
        return Task.FromResult(targetCode);
    }

    /// <summary>
    ///     获取站台
    /// </summary>
    /// <param name="deviceType"></param>
    /// <param name="tunnle"></param>
    /// <returns></returns>
    public Task<string> GetTranshipPositionAsync(DeviceTypeEnum deviceType, string tunnle, string region)
    {
        var controller = _deviceController
            .First(controller => controller.DeviceType == deviceType);
        var location = string.Empty;
        if (controller is AbstractStackerTranshipController targetInController)
            location = targetInController.GetCurrentPosWithTunnle(tunnle, region);
        return Task.FromResult(location);
    }

    /// <summary>
    ///     根据巷道获取设备
    /// </summary>
    /// <param name="deviceType"></param>
    /// <param name="tunnle"></param>
    /// <returns></returns>
    public Task<string> GetDeviceNameWithTunnleAsync(DeviceTypeEnum deviceType, string tunnle, string region)
    {
        var controller = _deviceController
            .First(controller => controller.DeviceType == deviceType);
        var deviceName = string.Empty;
        if (controller is AbstractStackerTranshipController targetInController)
            deviceName = targetInController.GetDeviceNameWithTunnle(tunnle, region);
        else if (controller is AbstractStackerController stackerController)
            deviceName = stackerController.GetDeviceNameWithTunnle(tunnle, region);
        return Task.FromResult(deviceName);
    }

    /// <summary>
    /// </summary>
    /// <param name="enable"></param>
    /// <param name="deviceType"></param>
    /// <param name="deviceName"></param>
    public void SetDviceEnable(bool enable, DeviceTypeEnum deviceType, string deviceName)
    {
        var controller = _deviceController
            .First(controller => controller.DeviceType == deviceType);
        controller.SetEnable(enable, deviceName);
    }

    public Task<string> GetDeviceNameWithTargetCodeAsync(DeviceTypeEnum deviceType, string pipLineCode, string region)
    {
        var controller = _deviceController
            .First(controller => controller.DeviceType == deviceType);
        var deviceName = string.Empty;
        if (controller is AbstractStockPortController targetController)
            deviceName = targetController.GetDeviceNameWithPipLine(pipLineCode, region);
        return Task.FromResult(deviceName);
    }
}