using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.Region.AddOrUpdate;
using Wcs.Contracts.Request.WcsTask;

namespace Wcs.Presentation.WcsTask;

public class AddWcsTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("wcstask/add-wcstask", [Authorize(Roles = "admin")] async (
            WcsTaskRequest request,
            ISender sender) =>
        {
            return await sender.Send(new AddOrUpdateRegionEvent());
        }).WithTags(AssemblyReference.WcsTask);
    }
}