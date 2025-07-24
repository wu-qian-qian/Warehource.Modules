using Common.Application.MediatR.Message;
using Plc.Contracts.Respon;
using Plc.Shared;

namespace Plc.Application.S7Plc.Get.Net;

public class GetS7NetQuery : IQuery<IEnumerable<S7NetDto>>
{
    public GetS7NetQuery()
    {
    }

    public GetS7NetQuery(Guid id, string ip, S7TypeEnum s7Type)
    {
        Id = id;
        Ip = ip;
        S7Type = s7Type;
    }

    public Guid Id { get; set; }

    public string Ip { get; set; }

    public S7TypeEnum S7Type { get; set; }
}