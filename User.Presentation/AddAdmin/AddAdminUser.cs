using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.Application.AddHandler;

namespace User.Presentation.AddAdmin;

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