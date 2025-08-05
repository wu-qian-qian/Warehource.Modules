using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Plc.Contracts.Respon;

namespace Plc.Application.DBHandler.Get.Entity;

public class GetS7EntityItemQuery : IQuery<Result<IEnumerable<S7EntityItemDto>>>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}