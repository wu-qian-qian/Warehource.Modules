using Common.Presentation.Endpoints;
using Identity.Application.Handler.Add.User;
using Identity.Contrancts.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Add;

internal class AddUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user/add-user", [Authorize(Roles = "admin")] async (AddUserRequst requst, ISender sender) =>
        {
            return await sender.Send(new AddUserEvent
            {
                Name = requst.Name,
                Email = requst.Email,
                Description = requst.Description,
                Phone = requst.Phone,
                Username = requst.Username,
                Password = requst.Password,
                RoleName = requst.RoleName
            });
        }).WithTags(AssemblyReference.User);
    }
}