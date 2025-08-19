namespace Wcs.Device.Abstract;

public abstract class BaseDeviceConfig
{
    protected BaseDeviceConfig()
    {
        Key = Guid.NewGuid().ToString();
        DBKey = Guid.NewGuid().ToString();
        TaskKey = Guid.NewGuid().ToString();
    }

    public string Code { get; set; }

    /// <summary>
    ///     缓存使用    唯一标识
    /// </summary>
    public string Key { get; protected set; }

    /// <summary>
    ///     用于DB块读取的key
    /// </summary>
    public string DBKey { get; protected set; }

    /// <summary>
    ///     用于任务获取的Key
    /// </summary>
    public string TaskKey { get; protected set; }
}