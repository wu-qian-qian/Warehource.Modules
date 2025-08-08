using System.Text.Json;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.Device.AddOrUpdate;
using Wcs.Contracts.Request.Device;

namespace Wcs.Presentation.Device;

public class InsertOrUpdate : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("decive/add-or-update", [Authorize(Roles = "admin")] async (
            DeviceRequest request,
            ISender sender) =>
        {
            var json = JsonSerializer.Serialize(request.Config);
            return await sender.Send(new AddOrUpdateDeviceEvent
            {
                DeviceName = request.DeviceName,
                DeviceType = request.DeviceType,
                Config = json,
                Enable = request.Enable,
                Description = request.Description,
                Id = request.Id.Value
            });
        }).WithTags(AssemblyReference.Decive);
    }
}