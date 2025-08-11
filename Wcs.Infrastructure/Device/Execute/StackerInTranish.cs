using Wcs.Device;
using Wcs.Device.Config;

namespace Wcs.Infrastructure.Device.Execute;

public class StackerInTranish : IDevice<StackerInTranishConfig>
{
    public string Name { get; }

    public StackerInTranishConfig Config { get; }
}