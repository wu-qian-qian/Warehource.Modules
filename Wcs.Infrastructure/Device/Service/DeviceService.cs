using Microsoft.Extensions.DependencyInjection;
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

    public async Task<RecommendTunnle> GerRecommendTunnleAsync(DeviceTypeEnum deviceType)
    {
        var controller = _deviceController
            .First(controller => controller.DeviceType == deviceType);
        var target = string.Empty;
        if (controller is AbstractStackerTranshipInController targetController)
        {
            var tunnels = targetController.GetReCommendTranship();

            //todo 发送WMS申请巷道
        }

        return new RecommendTunnle("1", "2");
    }
}