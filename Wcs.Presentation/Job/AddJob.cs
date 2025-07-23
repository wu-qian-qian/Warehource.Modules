using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.JobHandler.AddCommand;

namespace Wcs.Presentation.Job;

internal sealed class AddJobConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("job/add-job", async (
            AddJobRequest request,
            ISender sender) =>
        {
            return await sender.Send(new AddJobEvent
            {
                Name = request.Name, JobType = request.Jobtype,
                Description = request.Description, TimeOut = request.TimeOut, Timer = request.Timer,
                IsStart = request.IsStart
            });
        }).WithTags(AssemblyReference.Job);
    }
}

internal record AddJobRequest(
    string Name,
    string Description,
    string Jobtype,
    int TimeOut = 5000,
    int Timer = 3,
    bool IsStart = false);