using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace User.Presentation.Login;

internal class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user/user-login", [Authorize] async (ISender sender) =>
        {
            // return await sender.Send();
        }).WithTags(AssemblyReference.User);
    }
}

internal sealed class LoginRequst
{
    public string Username { get; set; }

    public string Password { get; set; }
}