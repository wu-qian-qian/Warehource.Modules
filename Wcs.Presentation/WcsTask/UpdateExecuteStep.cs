using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.WcsTask.UpdateExecute;
using Wcs.Contracts.Request.WcsTask;

namespace Wcs.Presentation.WcsTask;

public class UpdateExecuteStep : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("wcstask/update-wcstask-executestep", [Authorize] async (
              UpdateWcsTaskRequest request,
              ISender sender) =>
        {
            await sender.Send(new UpdateWcsTaskExecuteStepEvent
            {
                SerialNumber = request.SerialNumber,
                DeviceName = request.DeviceName
            });
        }).WithTags(AssemblyReference.WcsTask);
    }
}