using Common.Application.MediatR.Message;
using Wcs.Contracts.S7Plc;
using Wcs.Domain.S7;
using Wcs.Shared;

namespace Wcs.Application.S7Plc.Get;

public class GetS7NetQuery : IQuery<IEnumerable<S7NetDto>>
{
    public Guid Id { get; set; }

    public string Ip { get; set; }

    public S7TypeEnum S7Type { get; set; }
    public GetS7NetQuery()
    {
        
    }
    public GetS7NetQuery(Guid id,string ip, S7TypeEnum s7Type)
    {
        Id = id;
        Ip = ip;
        S7Type = s7Type;
    }
}