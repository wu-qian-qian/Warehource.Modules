using Common.Presentation.Endpoints;
using Identity.Application.Handler.Update.User;
using Identity.Contrancts.Request.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Update;

internal class UpdateUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user/update-user", [Authorize(Roles = "admin")]
            async (UpdateUserRequest request, ISender sender) =>
            {
                return await sender.Send(new UpdateUserCommand
                {
                    Id = request.Id,
                    Description = request.Description,
                    Email = request.Email,
                    LockoutEnd = request.LockoutEnd,
                    Name = request.Name,
                    Password = request.Password,
                    Phone = request.Phone,
                    RoleName = request.RoleName,
                    Username = request.Username
                });
            }).WithTags(AssemblyReference.User);
    }
}