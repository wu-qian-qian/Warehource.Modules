using Common.Presentation.Endpoints;
using Identity.Application.Handler.Get.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Get;

internal class GetUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("get/get-user/{username}",
                [Authorize] async (string username,ISender sender) =>
                {
                    return (await sender.Send(new GetUserQuery { UserName=username})).First();
                })
            .WithTags(AssemblyReference.User);
    }
}