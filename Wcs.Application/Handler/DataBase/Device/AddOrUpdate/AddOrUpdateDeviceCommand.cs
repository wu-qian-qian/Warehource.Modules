using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Device;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.Device.AddOrUpdate;

public class AddOrUpdateDeviceCommand : ICommand<Result<DeviceDto>>
{
    public Guid Id { get; set; }

    public DeviceTypeEnum? DeviceType { get; set; }

    public string? DeviceName { get; set; }

    public string? Description { get; set; }

    /// <summary>
    ///     配置
    /// </summary>
    public string? Config { get; set; }

    public bool? Enable { get; set; }

    public string RegionCode { get; set; }

    public string? GroupName { get; set; }
}