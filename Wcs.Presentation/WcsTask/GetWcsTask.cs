using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.WcsTask.Get;
using Wcs.Application.DBHandler.WcsTask.Insert;
using Wcs.Contracts.Request.WcsTask;

namespace Wcs.Presentation.WcsTask;

public class GetWcsTask
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("wcstask/get-wcstask", [Authorize] async (
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