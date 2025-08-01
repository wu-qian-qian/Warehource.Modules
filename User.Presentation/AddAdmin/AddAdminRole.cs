using Common.Presentation.Endpoints;
using Identity.Application.Handler.Add.Role;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.AddAdmin;

internal class AddAdminRole : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user/add-adminrole", async (ISender sender) =>
        {
            return await sender.Send(new AddRoleEvent
            {
                RoleName = "admin"
            });
        }).WithTags(AssemblyReference.User);
    }
}