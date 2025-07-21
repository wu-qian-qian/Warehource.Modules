using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using User.Application.GetQuery;

namespace User.Presentation.Get;

internal class GetUsers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("get/get-users", [Authorize(Roles = "admin")] async (ISender sender) =>
        {
             return await sender.Send(new GetUserQuery());
        }).WithTags(AssemblyReference.User);
    }
}