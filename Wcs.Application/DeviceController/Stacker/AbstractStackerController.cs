using Microsoft.Extensions.DependencyInjection;
using Wcs.Device.DeviceStructure.Stacker;

namespace Wcs.Application.DeviceController.Stacker;

public abstract class AbstractStackerController : BaseDeviceController<AbstractStacker>, IStackerController
{
    protected AbstractStackerController(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
    }


    public virtual async Task ExecuteAsync(CancellationToken token = default)
    {
        await InitializeAsync(token);
    }


    public string GetDeviceNameWithTunnle(string tunnle, string region)
    {
        var device = Devices
            .FirstOrDefault(d => (region.Contains(d.RegionCodes) || d.RegionCodes.Contains(region)) && d.Enable);
        return device?.Name;
    }
}