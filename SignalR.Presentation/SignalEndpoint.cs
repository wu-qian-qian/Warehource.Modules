using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using SignalR.Application.ChatHub;

namespace SignalR.Presentation
{
    public class SignalEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapHub<WcsHub>("/wcshub"); 
        }
    }
}
