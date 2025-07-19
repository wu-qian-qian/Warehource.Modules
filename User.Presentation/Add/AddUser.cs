using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.Application.AddHandler;

namespace User.Presentation.Add;

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

internal sealed class AddUserRequst
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string RoleName { get; set; }
}