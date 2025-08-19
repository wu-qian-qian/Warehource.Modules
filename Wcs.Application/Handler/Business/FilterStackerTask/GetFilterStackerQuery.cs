using Common.Application.MediatR.Message;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.FilterStackerTask;

public class GetFilterStackerQuery : IQuery<IEnumerable<WcsTask>>
{
    public DeviceTypeEnum? DeviceType { get; set; }

    public string? DeviceName { get; set; }
}