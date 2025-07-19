using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.JobHandler.DeleteCommand;

namespace Wcs.Presentation.Job;

internal sealed class DeleteJobConfig : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("job/delete-job", async (
            DeleteRequest request,
            ISender sender) =>
        {
            return await sender.Send(new DeleteJobEvent { Name = request.Name });
        }).WithTags(AssemblyReference.Wcs);
    }
}

internal record DeleteRequest(string Name);