using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Application.Handler.Business.CreatDeviceData;
using Wcs.Device.Device.StockPort;
using Wcs.Shared;

namespace Wcs.Application.Abstract.Device;

public abstract class AbstractStockPortController : IStockPortController
{
    protected readonly IServiceScopeFactory _scopeFactory;

    protected AbstractStockPortController(IServiceScopeFactory serviceScopeFactory)
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

    public void SetEnable(bool enable, string name)
    {
        if (Devices.Any(d => d.Name == name)) Devices.First().SetEnable(enable);
    }

    public string GetDeviceNameWithPipLine(string pipLineCode, string region)
    {
        var device = Devices.FirstOrDefault(p => p.CanRegionExecute(region) && p.Config.PipeLineCode == pipLineCode);
        return device?.Name;
    }
}