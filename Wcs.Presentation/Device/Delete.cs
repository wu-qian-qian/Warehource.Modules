using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.Device.Delete;

namespace Wcs.Presentation.Device;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("decive/delete/{id}", [Authorize(Roles = "admin")] async (
            Guid request,
            ISender sender) =>
        {
            return await sender.Send(new DeleteDeviceEvent
            {
                Id = request
            });
        }).WithTags(AssemblyReference.Decive);
    }
}