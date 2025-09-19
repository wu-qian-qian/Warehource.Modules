using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.Region.Get;

namespace Wcs.Presentation.Region;

public class GetRegionCode : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("region/get-regionCode",
                [Authorize] async (ISender sender) =>
                {
                    var result = await sender.Send(new GetRegionQuery());
                    var res = result.Select(p => p.Code);
                    return res;
                })
            .WithTags(AssemblyReference.Region);
    }
}