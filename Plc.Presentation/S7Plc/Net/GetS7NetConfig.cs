using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.PlcEvent.Get.Net;
using Plc.Application.ReadPlc;

namespace Plc.Presentation.S7Plc.Net;

internal class GetS7NetConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("plc/get-allplc",
                async (ISender sender) =>
                {
                    await sender.Send(new PlcEventCommand
                    {
                        Ip = "127.0.0.1"
                    });
                    return await sender.Send(new GetS7NetQuery());
                })
            .WithTags(AssemblyReference.Plc);
    }
}