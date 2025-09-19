using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.Device.Get;

namespace Wcs.Presentation.Device;

public class GetDeviceGroup : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("decive/get-group-code", [Authorize] async (ISender sender) =>
        {
            var devices = await sender.Send(new GetDeviceQuery());
            var deviceGroups = devices.Value.Select(p => p.GroupName);
            return deviceGroups;
        }).WithTags(AssemblyReference.Decive);
    }
}