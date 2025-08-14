using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DB.Job.Insert;
using Wcs.Contracts.Request.Job;

namespace Wcs.Presentation.Job.Add;

internal sealed class AddJobConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("job/add-job", [Authorize(Roles = "admin")] async (
            AddJobRequest request,
            ISender sender) =>
        {
            return await sender.Send(new AddJobCommand
            {
                Name = request.Name, JobType = request.Jobtype,
                Description = request.Description, TimeOut = request.TimeOut, Timer = request.Timer,
                IsStart = request.IsStart
            });
        }).WithTags(AssemblyReference.Job);
    }
}