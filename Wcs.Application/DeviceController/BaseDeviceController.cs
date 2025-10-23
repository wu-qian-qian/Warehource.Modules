using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Handler.Business.CreatDeviceData;
using Wcs.Device.Abstract;
using Wcs.Shared;

namespace Wcs.Application.DeviceController;

/// <summary>
///     控制中心的公共操作抽象
/// </summary>
/// <typeparam name="TDeviceStructure"></typeparam>
public abstract class BaseDeviceController<TDeviceStructure> where TDeviceStructure : class, IDevice
{
    protected readonly IServiceScopeFactory _scopeFactory;

    protected BaseDeviceController(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public TDeviceStructure[] Devices { get; protected set; }

    public DeviceTypeEnum DeviceType { get; init; }

    protected async ValueTask InitializeAsync(CancellationToken token = default)
    {
        if (Devices == null || Devices.Length == 0)
        {
            using var scope = _scopeFactory.CreateScope();
            var sender = scope.ServiceProvider.GetService<ISender>();
            var res = await sender.Send(new CreatDeviceDataCommand
            {
                DeviceType = DeviceType
            });
            //安全转换
            Devices = res.OfType<TDeviceStructure>().ToArray();
        }
    }

    public void SetEnable(bool enable, string name)
    {
        if (Devices.Any(d => d.Name == name)) Devices.First().SetEnable(enable);
    }
}