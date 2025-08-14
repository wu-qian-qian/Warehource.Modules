using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DB.WcsTask.Delete;

namespace Wcs.Presentation.WcsTask;

public class DeleteWcsTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("wcstask/delete-wcstask/{serialNumber}", [Authorize] async (
            int request,
            ISender sender) =>
        {
            return await sender.Send(new DeleteWcsTaskCommand
            {
                SerialNumber = request
            });
        }).WithTags(AssemblyReference.WcsTask);
    }
}