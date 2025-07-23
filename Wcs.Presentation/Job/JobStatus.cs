using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.JobHandler.SetStatusCommand;

namespace Wcs.Presentation.Job;

internal sealed class JobStatusConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("job/job-status", async (
            StatusRequest request,
            ISender sender) =>
        {
            return await sender.Send(new StatusJobEvent { Name = request.Name, Status = request.Status });
        }).WithTags(AssemblyReference.Job);
    }
}

internal record StatusRequest(string Name, bool Status);