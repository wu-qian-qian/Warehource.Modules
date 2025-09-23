using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Plc.Contracts.Respon;

namespace Plc.Application.Handler.DataBase.Get.Entity;

public class GetS7EntityItemQuery : IQuery<Result<IEnumerable<S7EntityItemDto>>>
{
    public string? Ip { get; set; }

    public string? DeviceName { get; set; }

    public string? Name { get; set; }
}