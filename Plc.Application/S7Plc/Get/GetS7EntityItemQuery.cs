using Common.Application.MediatR.Message;
using Plc.Contracts.S7Plc;

namespace Plc.Application.S7Plc.Get;

public class GetS7EntityItemQuery : IQuery<IEnumerable<S7EntityItemDto>>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}