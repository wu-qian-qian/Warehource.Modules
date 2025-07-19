using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.Application.AddHandler;

namespace User.Presentation.AddAdmin;

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