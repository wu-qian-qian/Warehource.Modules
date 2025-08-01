using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.DBHandler.Get.Net;

namespace Plc.Presentation.S7Plc.Net;

internal class GetS7NetConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("plc/get-allplc",
                async (ISender sender) =>
                {
                    return await sender.Send(new GetS7NetQuery());
                })
            .WithTags(AssemblyReference.Plc);
    }
}