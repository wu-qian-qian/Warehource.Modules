using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Plc.Contracts.Respon;
using Plc.Shared;

namespace Plc.Application.Handler.DataBase.Get.Net;

public class GetS7NetQuery : IQuery<Result<IEnumerable<S7NetDto>>>
{
    public Guid? Id { get; set; }

    public string? Ip { get; set; }

    public S7TypeEnum? S7Type { get; set; }
}