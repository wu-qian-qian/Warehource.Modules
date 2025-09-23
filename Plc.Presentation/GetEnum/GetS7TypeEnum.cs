using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Shared;

namespace Plc.Presentation.GetEnum;

internal class GetS7TypeEnum : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("plc/get-s7Type-Enum",
                [Authorize](ISender sender) =>
                {
                    var res = typeof(S7TypeEnum).GetEnumNames();
                    return res;
                })
            .WithTags(AssemblyReference.Enum);
    }
}