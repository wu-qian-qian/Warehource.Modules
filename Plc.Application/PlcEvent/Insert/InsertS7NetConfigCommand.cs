using Common.Application.MediatR.Message;
using Plc.Contracts.Request;
using Plc.Contracts.Respon;

namespace Plc.Application.PlcEvent.Insert;

public class InsertS7NetConfigCommand : ICommand<IEnumerable<S7NetDto>>
{
    public IEnumerable<S7NetRequest> S7NetRequests { get; set; }

    public IEnumerable<S7NetEntityItemRequest> S7NetEntityItemRequests { get; set; }
}