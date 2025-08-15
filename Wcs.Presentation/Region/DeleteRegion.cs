using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.Region.Delete;

namespace Wcs.Presentation.Region;

public class DeleteRegion : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("region/delete-region/{id}", [Authorize(Roles = "admin")] async (
            Guid id,
            ISender sender) =>
        {
            return await sender.Send(new DeleteRegionCommand
            {
                Id = id
            });
        }).WithTags(AssemblyReference.Region);
    }
}