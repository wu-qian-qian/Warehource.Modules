using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.Region.AddOrUpdate;
using Wcs.Contracts.Request.Region;

namespace Wcs.Presentation.Region;

public class UpdateRegion : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("region/add-region", [Authorize(Roles = "admin")] async (
            RegionRequest request,
            ISender sender) =>
        {
            return await sender.Send(new AddOrUpdateRegionEvent
            {
                Code = request.Code,
                Description = request.Description
            });
        }).WithTags(AssemblyReference.Region);
    }
}