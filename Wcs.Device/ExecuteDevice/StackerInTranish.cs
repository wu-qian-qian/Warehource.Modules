using Wcs.Device.Config;

namespace Wcs.Device.ExecuteDevice;

public class StackerInTranish : IDevice<StackerInTranishConfig>
{
    public string Name { get; }

    public StackerInTranishConfig Config { get; }
}