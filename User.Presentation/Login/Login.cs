using Common.Application.Encodings;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.Application.LoginHandler;

namespace User.Presentation.Login;

internal class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user/user-login", [AllowAnonymous]
            async (ISender sender, LoginRequst requst, ITokenService tokenService) =>
            {
                var userdto = await sender.Send(new LoginEvent
                {
                    Username = requst.Username,
                    Password = requst.Password
                });
                return tokenService.BuildJwtString([userdto.RoleName], [userdto.Name]);
            }).WithTags(AssemblyReference.User);
    }
}

internal sealed class LoginRequst
{
    public string Username { get; set; }

    public string Password { get; set; }
}