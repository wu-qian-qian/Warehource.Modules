using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Delete;

internal class DeleteUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("user/Delete-user/id", [Authorize(Roles = "admin")] async (Guid request, ISender sender) =>
        {
            // return await sender.Send();
        }).WithTags(AssemblyReference.User);
    }
}