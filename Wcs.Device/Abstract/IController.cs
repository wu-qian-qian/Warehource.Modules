using NPOI.SS.Formula.Functions;
using Wcs.Shared;

namespace Wcs.Device.Abstract;

public interface IController<T> : IController where T : class
{
    T[] Devices { get; }
    DeviceTypeEnum DeviceType { get; }
    Task ExecuteAsync(CancellationToken token = default);
}

public interface IController
{
}