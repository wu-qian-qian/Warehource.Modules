using Common.Application.MediatR.Message.PageQuery;
using Plc.Contracts.Respon;
using Plc.Shared;

namespace Plc.Application.Handler.DataBase.Page.S7Net;

public class GetS7NetPageCommand : PageQuery<S7NetDto>
{
    public string? Ip { get; set; }

    public S7TypeEnum? S7Type { get; set; }
}