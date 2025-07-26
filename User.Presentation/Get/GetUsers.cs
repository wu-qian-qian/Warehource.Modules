using Common.Presentation.Endpoints;
using Identity.Application.GetQuery;
using Identity.Application.GetQuery.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Presentation.Get;

internal class GetUsers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("get/get-users",
                [Authorize(Roles = "admin")] async (ISender sender) =>
                {
                    return await sender.Send(new GetUserQuery());
                })
            .WithTags(AssemblyReference.User);
    }
}