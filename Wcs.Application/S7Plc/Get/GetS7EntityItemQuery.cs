using Common.Application.MediatR.Message;
using Wcs.Contracts.S7Plc;

namespace Wcs.Application.S7Plc.Get;

public class GetS7EntityItemQuery:IQuery<IEnumerable<S7EntityItemDto>>
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
}