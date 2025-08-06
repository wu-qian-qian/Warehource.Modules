using Common.Application.MediatR.Behaviors;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.Job.Get;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Presentation.Job.Get;

internal sealed class GetJobsConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("job/get-jobs", async (ISender sender) =>
            {
                var result = new Result<IEnumerable<JobDto>>();
                var data = await sender.Send(new GetAllJobQuery());
                result.SetValue(data);
                return result;
            })
            .WithTags(AssemblyReference.Job);
    }
}