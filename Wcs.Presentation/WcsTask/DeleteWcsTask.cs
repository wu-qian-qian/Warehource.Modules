using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.WcsTask.Get;
using Wcs.Contracts.Request.WcsTask;

namespace Wcs.Presentation.WcsTask;

public class DeleteWcsTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("wcstask/cancel-wcstask", [Authorize] async (
            GetWcsTaskRequest request,
            ISender sender) =>
        {
            return await sender.Send(new GetWcsTaskQuery
            {
                Container = request.Container,
                CreatorSystemType = request.CreatorSystemType,
                EndTime = request.EndTime,
                StartTime = request.StartTime,
                TaskCode = request.TaskCode,
                SerialNumber = request.SerialNumber,
                TaskStatus = request.TaskStatus
            });
        }).WithTags(AssemblyReference.WcsTask);
    }
}