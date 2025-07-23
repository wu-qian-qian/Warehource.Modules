using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Delete;

internal class DeleteRole : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("user/Delete-role/{roleName}",
                [Authorize(Roles = "admin")] async (string request, ISender sender) => { })
            .WithTags(AssemblyReference.User);
    }
}