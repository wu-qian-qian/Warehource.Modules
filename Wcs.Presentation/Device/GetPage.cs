using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.Device.GetPage;
using Wcs.Contracts.Request.Device;

namespace Wcs.Presentation.Device;

internal class GetPage : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("decive/get-page", [Authorize] async (GetDevicePageRequest request, ISender sender) =>
        {
            return await sender.Send(new GetDevicePageCommand
            {
                DeviceName = request.DeviceName,
                DeviceType = request.DeviceType,
                Enable = request.Enable,
                Total = request.Total,
                PageIndex = request.PageIndex
            });
        }).WithTags(AssemblyReference.Decive);
    }
}