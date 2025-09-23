using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.Handler.DataBase.Page.S7Net;
using Plc.Contracts.Request;

namespace Plc.Presentation.S7Plc.Page;

internal class GetS7NetPage : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("plc/get-plcnet-page",
            [Authorize] async (GetS7NetPageRequest request, ISender sender) =>
            {
                return await sender.Send(new GetS7NetPageCommand
                {
                    Ip = request.Ip,
                    Total = request.Total,
                    PageIndex = request.PageIndex,
                    S7Type = request.S7Type,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime
                });
            }).WithTags(AssemblyReference.Plc);
    }
}