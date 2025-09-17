using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.Region.Get;

namespace Wcs.Presentation.Region;

public class GetRegion : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("region/get-region",
                [Authorize] async (ISender sender) => { return await sender.Send(new GetRegionQuery()); })
            .WithTags(AssemblyReference.Region);
    }
}