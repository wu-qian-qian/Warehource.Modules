namespace Wcs.Application.Abstract.Device.BaseExecute;

/// <summary>
///     堆垛机调度中心
/// </summary>
public interface IStackerControler : IControl
{
    Task ExecuteAsync(CancellationToken token = default);
}