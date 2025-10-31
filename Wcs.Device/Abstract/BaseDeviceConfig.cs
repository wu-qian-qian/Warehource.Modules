namespace Wcs.Device.Abstract;

public abstract class BaseDeviceConfig
{
    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            InitKeys();
        }
    }

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

    private void InitKeys()
    {
        Key = $"{Name.GetHashCode()}{nameof(Key).GetHashCode()}";
        DBKey = $"{Name.GetHashCode()}{nameof(DBKey).GetHashCode()}";
        TaskKey = $"{Name.GetHashCode()}{nameof(TaskKey).GetHashCode()}";
    }
}