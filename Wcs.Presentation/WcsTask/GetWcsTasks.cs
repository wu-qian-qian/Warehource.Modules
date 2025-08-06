using Common.Application.MediatR.Behaviors;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.WcsTask.Get;
using Wcs.Contracts.Request.WcsTask;
using Wcs.Contracts.Respon.WcsTask;

namespace Wcs.Presentation.WcsTask;

public class GetWcsTasks : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("wcstask/get-wcstasks", [Authorize] async (
            GetWcsTaskRequest request,
            ISender sender) =>
        {
            Result<IEnumerable<WcsTaskDto>> result = new();
            var data = await sender.Send(new GetWcsTaskQuery
            {
                Container = request.Container,
                CreatorSystemType = request.CreatorSystemType,
                EndTime = request.EndTime,
                StartTime = request.StartTime,
                TaskCode = request.TaskCode,
                SerialNumber = request.SerialNumber,
                TaskStatus = request.TaskStatus
            });
            result.SetValue(data);
            return result;
        }).WithTags(AssemblyReference.WcsTask);
    }
}