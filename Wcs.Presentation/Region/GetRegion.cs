using Common.Application.MediatR.Behaviors;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.Region.Get;
using Wcs.Contracts.Respon.Region;

namespace Wcs.Presentation.Region;

public class GetRegion : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("region/get-region", [Authorize] async (
            ISender sender) =>
        {
            Result<IEnumerable<RegionDto>> result = new();
            var data = await sender.Send(new GetRegionQuery());
            result.SetValue(data);
            return result;
        }).WithTags(AssemblyReference.Region);
    }
}