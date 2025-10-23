using Microsoft.Extensions.DependencyInjection;
using Wcs.Device.DeviceStructure.StockPort;

namespace Wcs.Application.DeviceController.StockPort;

public abstract class AbstractStockPortController : BaseDeviceController<AbstractStockPort>, IStockPortController
{
    protected AbstractStockPortController(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
    {
    }


    public virtual async Task ExecuteAsync(CancellationToken token = default)
    {
        await InitializeAsync(token);
    }


    public string GetDeviceNameWithPipLine(string pipLineCode, string region)
    {
        var device = Devices.FirstOrDefault(p => p.CanRegionExecute(region) && p.Config.PipeLineCode == pipLineCode);
        return device?.Name;
    }
}