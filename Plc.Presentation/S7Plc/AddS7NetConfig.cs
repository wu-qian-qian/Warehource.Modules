using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Plc.Presentation.S7Plc;

internal class AddS7NetConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("plc/add-allplc-centity-item", async (
            IFormFile file, ISender sender) =>
        {
        }).WithTags(AssemblyReference.Plc);
    }
}