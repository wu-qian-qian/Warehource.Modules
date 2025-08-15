using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.Device.Delete;

namespace Wcs.Presentation.Device;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("decive/delete/{id}", [Authorize(Roles = "admin")] async (
            Guid request,
            ISender sender) =>
        {
            return await sender.Send(new DeleteDeviceCommand
            {
                Id = request
            });
        }).WithTags(AssemblyReference.Decive);
    }
}