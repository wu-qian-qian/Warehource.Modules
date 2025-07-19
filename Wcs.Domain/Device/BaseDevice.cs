using Common.Domain.EF;

namespace Wcs.Domain.Device;

public class BaseDevice : IEntity
{
    public BaseDevice(Guid id) : base(id)
    {
    }
}