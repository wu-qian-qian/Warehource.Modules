namespace UI.Model.Device;

public class DeviceRequest
{
    public Guid? Id { get; set; }

    public string? DeviceType { get; set; }

    public string? DeviceName { get; set; }

    public string? Description { get; set; }

    public string? RegionCodes { get; set; }

    /// <summary>
    ///     配置
    /// </summary>
    public object? Config { get; set; }


    public string? GroupName { get; set; }
    public bool Enable { get; set; }
}