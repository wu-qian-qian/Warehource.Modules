using Common.Application.Encodings;
using Common.Presentation.Endpoints;
using Identity.Application.Handler.Login;
using Identity.Contrancts.Request;
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
                return await sender.Send(new LoginEvent
                {
                    Username = requst.Username,
                    Password = requst.Password
                });
              
            }).WithTags(AssemblyReference.User);
    }
}