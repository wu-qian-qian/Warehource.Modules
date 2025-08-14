using Wcs.Device.Config;
using Wcs.Device.Device.BaseExecute;

namespace Wcs.Infrastructure.Device.Execute.Stacker;

/// <summary>
///     堆垛机数据结构
/// </summary>
public class Stacker : AbstractStacker
{
    public Stacker(string name, StackerConfig config)
    {
        Name = name;
        Config = config;
    }
}