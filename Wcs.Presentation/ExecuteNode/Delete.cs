using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DB.ExecueNode.Delete;

namespace Wcs.Presentation.ExecuteNode;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("executenode/delete/{id}", [Authorize(Roles = "admin")] async (
            Guid request,
            ISender sender) =>
        {
            return await sender.Send(new DeleteExecuteNodeCommand
            {
                Id = request
            });
        }).WithTags(AssemblyReference.ExecuteNode);
    }
}