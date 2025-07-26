using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.PlcEvent.Get.Entity;

namespace Plc.Presentation.S7Plc.Entity;

internal class GetS7EntityItem : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("plc/get-allplc-centity-item",
                async (ISender sender) => { return await sender.Send(new GetS7EntityItemQuery()); })
            .WithTags(AssemblyReference.Plc);
    }
}