using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.Application.GetQuery;

namespace User.Presentation.Get;

internal class GetRoles : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("user/get-roles",
                [Authorize] async (ISender sender) => { return await sender.Send(new GetRoleQuery()); })
            .WithTags(AssemblyReference.User);
    }
}