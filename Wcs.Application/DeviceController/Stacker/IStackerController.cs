using Wcs.Device.Abstract;
using Wcs.Device.DeviceStructure.Stacker;

namespace Wcs.Application.DeviceController.Stacker;

/// <summary>
///     堆垛机调度中心
/// </summary>
public interface IStackerController
    : IController<AbstractStacker>
{
}