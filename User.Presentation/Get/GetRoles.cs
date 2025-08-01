using Common.Presentation.Endpoints;
using Identity.Application.Handler.Get.Role;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Get;

internal class GetRoles : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("user/get-roles",
                [Authorize] async (ISender sender) => { return await sender.Send(new GetRoleQuery()); })
            .WithTags(AssemblyReference.User);
    }
}