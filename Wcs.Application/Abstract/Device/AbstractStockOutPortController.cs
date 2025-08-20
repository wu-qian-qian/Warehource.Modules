using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Handler.Business.CreatDeviceData;
using Wcs.Device.Device.StockPort;
using Wcs.Shared;

namespace Wcs.Application.Abstract.Device;

public abstract class AbstractStockOutPortController : IStockPortController
{
    protected readonly IServiceScopeFactory _scopeFactory;

    protected AbstractStockOutPortController(IServiceScopeFactory serviceScopeFactory)
    {
        DeviceType = DeviceTypeEnum.StackerOutTranShip;
    }

    public AbstractStockPort[] Devices { get; private set; }

    public DeviceTypeEnum DeviceType { get; init; }


    public virtual async Task ExecuteAsync(CancellationToken token = default)
    {
        if (Devices == null || Devices.Length == 0)
        {
            using var scope = _scopeFactory.CreateScope();
            var sender = scope.ServiceProvider.GetService<ISender>();
            Devices = (AbstractStockPort[])await sender.Send(new CreatDeviceDataCommand
            {
                DeviceType = DeviceType
            });
        }
    }
}