using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.S7Plc.Insert;

namespace Plc.Presentation.S7Plc;

internal class AddS7NetConfig : IEndpoint
{
    /// <summary>
    ///     nimiApi限制IformFile必须要防伪
    /// </summary>
    /// <param name="app"></param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("plc/add-allplc-config", async (
                IFormFile file, ISender sender) =>
            {
                var stream = file.OpenReadStream();
                await sender.Send(
                    new InsertS7NetConfigCommand
                    {
                        Stream = stream
                    }
                );
            }
        ).DisableAntiforgery().WithTags(AssemblyReference.Plc);
    }
}