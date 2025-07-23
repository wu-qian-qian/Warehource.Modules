using Common.Application.Encodings;
using Common.Presentation.Endpoints;
using Identity.Application.LoginHandler;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Login;

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