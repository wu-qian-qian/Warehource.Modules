namespace UI.Model.Device;

public class DeviceModel : BaseModel
{
    public string DeviceType { get; set; }

    public string DeviceName { get; set; }

    public string? Description { get; set; }

    /// <summary>
    ///     配置
    /// </summary>
    public string Config { get; set; }

    /// <summary>
    ///     区域字符组
    /// </summary>

    public string RegionCode { get; set; }

    public bool Enable { get; set; }
}