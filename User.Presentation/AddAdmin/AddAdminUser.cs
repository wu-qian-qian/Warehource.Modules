using Common.Presentation.Endpoints;
using Identity.Application.AddHandler;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.AddAdmin;

internal class AddAdminUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user/add-adminuser", async (ISender sender) =>
        {
            return await sender.Send(new AddUserEvent
            {
                Name = "管理员",
                Username = "admin",
                Password = "qwe",
                RoleName = "admin"
            });
        }).WithTags(AssemblyReference.User);
    }
}