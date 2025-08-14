using Common.Application.MediatR.Message;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Execute.GetWcsTask;

public class GetWcsTaskQuery : IQuery<IEnumerable<WcsTask>>
{
    public string? DeviceName { get; set; }
    public DeviceTypeEnum? DeviceType { get; set; }

    /// <summary>
    ///     是否堆垛机取货位
    /// </summary>
    public bool IsTranShipPoint { get; set; }

    public int? Tunnle { get; set; }
}