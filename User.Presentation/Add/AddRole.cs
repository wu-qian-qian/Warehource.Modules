using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.Application.AddHandler;

namespace User.Presentation.Add;

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

public record AddRoleRequest
{
    public string RoleName { get; set; }
    public string Description { get; set; }
}