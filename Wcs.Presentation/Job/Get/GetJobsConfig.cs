using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.Job.Get;

namespace Wcs.Presentation.Job.Get;

internal sealed class GetJobsConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("job/get-jobs", async (ISender sender) => { return await sender.Send(new GetAllJobQuery()); })
            .WithTags(AssemblyReference.Job);
    }
}