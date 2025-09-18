using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.WcsTask.UpdateTask;
using Wcs.Contracts.Request.WcsTask;

namespace Wcs.Presentation.WcsTask;

public class UpdateTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("wcstask/update-wcstask-status", [Authorize] 
        async (UpdateWcsTaskRequest request,ISender sender) =>
        {
            await sender.Send(new UpdateWcsTaskCommand
            {
                SerialNumber = request.SerialNumber,
                WcsTaskStatusType = request.TaskStatus,
                DeviceName = request.DeviceName,
                GetLocation = request.GetLocation,
                PutLocation = request.PutLocation,
                IsEnforce=request.IsEnforce,
                Level=request.Level
            });
        }).WithTags(AssemblyReference.WcsTask);
    }
}