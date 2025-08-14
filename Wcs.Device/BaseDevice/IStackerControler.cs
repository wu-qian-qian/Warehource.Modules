using Wcs.Device.Abstract;

namespace Wcs.Device.BaseDevice;

/// <summary>
///     堆垛机调度中心
/// </summary>
public interface IStackerControler : IControl
{
    Task ExecuteAsync(CancellationToken token = default);
}