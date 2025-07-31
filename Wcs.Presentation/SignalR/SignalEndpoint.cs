using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.SignalR.ChatHub;

namespace Wcs.Presentation.SignalR;

public class SignalEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapHub<WcsHub>("/wcshub");
    }
}