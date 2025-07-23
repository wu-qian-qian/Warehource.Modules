using Common.Presentation.Endpoints;
using Identity.Application.AddHandler;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Add;

internal class AddRole : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user/add-role", [Authorize(Roles = "admin")] async (AddRoleRequest request, ISender sender) =>
        {
            return await sender.Send(new AddRoleEvent
            {
                RoleName = request.RoleName,
                Description = request.Description
            });
        }).WithTags(AssemblyReference.User);
    }
}