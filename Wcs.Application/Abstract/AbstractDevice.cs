using Wcs.Device;

namespace Wcs.Application.Abstract;

public abstract class AbstractDevice<T, TDBEntity> : IDevice<T> where T : class where TDBEntity : BaseEntity
{
    public abstract string Name { get; init; }
    public abstract T Config { get; init; }

    public abstract Task ExecuteAsync(CancellationToken token = default);
}