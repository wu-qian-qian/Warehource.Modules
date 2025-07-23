using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.S7Plc.Get;
using Wcs.Presentation.Job;

namespace Wcs.Presentation.S7Plc;

internal class GetS7NetConfig:IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("plc/get-allplc", async (
            S7NetRequest request,ISender sender) =>
        {
            return await sender.Send(new GetS7NetQuery(request.Id,request.Ip,request.S7Type));
        }).WithTags(AssemblyReference.Plc);
    }
}

internal class GetS7EntityItem:IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("plc/get-allplc-centity-item", async (
            S7NetEntityItemRequest requestr,  ISender sender) =>
        {
            return await sender.Send(new GetS7EntityItemQuery()
            {
               
            });
        }).WithTags(AssemblyReference.Plc);
    }
}