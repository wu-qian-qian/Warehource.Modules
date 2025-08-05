using Common.Application.MediatR.Behaviors;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.Job.Get;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Presentation.Job.Get;

internal sealed class GetJobConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("job/get-job/{id}", [Authorize] async (
            Guid id,
            ISender sender) =>
        {
            var data= await sender.Send(new GetJobQuery(id));
            var result = new Result<JobDto>();
            result.SetValue(data);
            return result;
        }).WithTags(AssemblyReference.Job);
    }
}