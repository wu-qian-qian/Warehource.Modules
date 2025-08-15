using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.Device.Get;
using Wcs.Contracts.Request.Device;

namespace Wcs.Presentation.Device;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("decive/get", [Authorize] async (GetDviceRequest request, ISender sender) =>
        {
            return await sender.Send(new GetDeviceQuery
            {
                DeviceName = request.DeviceName,
                DeviceType = request.DeviceType,
                Enable = request.Enable
            });
        }).WithTags(AssemblyReference.Decive);
    }
}