using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.Job.SetStatus;
using Wcs.Contracts.Request.Job;

namespace Wcs.Presentation.Job.Status;

internal sealed class JobStatusConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("job/job-status", [Authorize(Roles = "admin")] async (
            StatusRequest request,
            ISender sender) =>
        {
            return await sender.Send(new StatusJobEvent { Name = request.Name, Status = request.Status });
        }).WithTags(AssemblyReference.Job);
    }
}