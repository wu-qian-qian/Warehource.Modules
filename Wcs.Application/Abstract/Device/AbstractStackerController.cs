using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Handler.Business.CreatDeviceData;
using Wcs.Device.Device.Stacker;
using Wcs.Shared;

namespace Wcs.Application.Abstract.Device;

public abstract class AbstractStackerController : IStackerController
{
    protected readonly IServiceScopeFactory _scopeFactory;

    protected AbstractStackerController(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public AbstractStacker[] Devices { get; private set; }

    public DeviceTypeEnum DeviceType { get; init; }

    public virtual async Task ExecuteAsync(CancellationToken token = default)
    {
        if (Devices == null || Devices.Length == 0)
        {
            using var scope = _scopeFactory.CreateScope();
            var sender = scope.ServiceProvider.GetService<ISender>();
            Devices = (AbstractStacker[])await sender.Send(new CreatDeviceDataCommand
            {
                DeviceType = DeviceType
            });
        }
    }
}