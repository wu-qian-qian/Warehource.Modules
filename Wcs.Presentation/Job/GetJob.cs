using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.JobHandler.GetQuery;

namespace Wcs.Presentation.Job;

internal sealed class GetJobConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("job/get-job/{id}", async (
            Guid id,
            ISender sender) =>
        {
            return await sender.Send(new GetJobQuery(id));
        }).WithTags(AssemblyReference.Job);
    }
}