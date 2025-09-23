using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.Handler.DataBase.Page.Entity;
using Plc.Contracts.Request;

namespace Plc.Presentation.S7Plc.Page;

internal class GetEntityItemPage : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("plc/get-entityitem-page",
            [Authorize] async (GetEntityItemPageRequest request, ISender sender) =>
            {
                return await sender.Send(new GetEntityItemPageCommand
                {
                    Name = request.Name,
                    DeviceName = request.DeviceName,
                    Ip = request.Ip,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    PageIndex = request.PageIndex,
                    Total = request.Total
                });
            }).WithTags(AssemblyReference.Plc);
    }
}