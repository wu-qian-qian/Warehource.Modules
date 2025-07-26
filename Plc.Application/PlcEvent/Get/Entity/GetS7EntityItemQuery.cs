using Common.Application.MediatR.Message;
using Plc.Contracts.Respon;

namespace Plc.Application.PlcEvent.Get.Entity;

public class GetS7EntityItemQuery : IQuery<IEnumerable<S7EntityItemDto>>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}