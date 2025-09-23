using Common.Application.MediatR.Message.PageQuery;
using Plc.Contracts.Respon;

namespace Plc.Application.Handler.DataBase.Page.Entity;

public class GetEntityItemPageCommand : PageQuery<S7EntityItemDto>
{
    public string? Ip { get; set; }

    public string? DeviceName { get; set; }

    public string? Name { get; set; }
}