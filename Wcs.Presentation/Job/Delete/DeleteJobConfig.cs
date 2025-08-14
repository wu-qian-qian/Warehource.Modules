using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DB.Job.Delete;
using Wcs.Contracts.Request.Job;

namespace Wcs.Presentation.Job.Delete;

internal sealed class DeleteJobConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("job/delete-job", [Authorize(Roles = "admin")] async (
            DeleteRequest request,
            ISender sender) =>
        {
            return await sender.Send(new DeleteJobCommand { Name = request.Name });
        }).WithTags(AssemblyReference.Job);
    }
}