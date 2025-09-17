using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.Job.Update;
using Wcs.Contracts.Request.Job;

namespace Wcs.Presentation.Job.Update;

internal sealed class JobStatusConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("job/job-status", [Authorize(Roles = "admin")] async (
            UpdateJobRequest request,
            ISender sender) =>
        {
            return await sender.Send(new UpdateJobCommand(request.Name, request.IsStart, request.Timer,
                request.TimerOut, request.Description));
        }).WithTags(AssemblyReference.Job);
    }
}