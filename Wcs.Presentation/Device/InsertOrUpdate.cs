using System.Text.Json;
using Common.Application.MediatR.Behaviors;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.Device.AddOrUpdate;
using Wcs.Contracts.Request.Device;
using Wcs.Contracts.Respon.Device;

namespace Wcs.Presentation.Device;

public class InsertOrUpdate : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("decive/add-or-update", [Authorize(Roles = "admin")]
            async (DeviceRequest request, ISender sender) =>
            {
                var result = default(Result<DeviceDto>);
                if (request.Config != null)
                {
                    var json = JsonSerializer.Serialize(request.Config);
                    result = await sender.Send(new AddOrUpdateDeviceCommand
                    {
                        DeviceName = request.DeviceName,
                        DeviceType = request.DeviceType,
                        Config = json,
                        Enable = request.Enable,
                        Description = request.Description,
                        Id = request.Id,
                        GroupName = request.GroupName,
                        RegionCode = request.RegionCodes
                    });
                }
                else
                {
                    result = new Result<DeviceDto>();
                    result.SetMessage("请保存配置文件");
                }

                return result;
            }).WithTags(AssemblyReference.Decive);
    }
}