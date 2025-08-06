using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.WcsTask.UpdateStatus;
using Wcs.Contracts.Request.WcsTask;

namespace Wcs.Presentation.WcsTask;

public class UdateTaskStatus : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("wcstask/update-wcstask-status", [Authorize] async (
            UpdateWcsTaskRequest request,
            ISender sender) =>
        {
            await sender.Send(new UpdateWcsTaskStatusEvent
            {
                SerialNumber = request.SerialNumber,
                WcsTaskStatusType = request.WcsTaskStatusType
            });
        }).WithTags(AssemblyReference.WcsTask);
    }
}