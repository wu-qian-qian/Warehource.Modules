using Common.Domain.Event;

namespace Plc.CustomEvents;

public class S7WritePlcDataBlockIntegrationEvent : IMassTransitDomainEvent
{
    public S7WritePlcDataBlockIntegrationEvent(DateTime occurredOnUtc) : base(occurredOnUtc)
    {
    }

    public S7WritePlcDataBlockIntegrationEvent(DateTime occurredOnUtc, string deviceName,
        Dictionary<string, string> dBNameToDataValue, string key) : base(occurredOnUtc)
    {
        DeviceName = deviceName;
        DbNameToDataValue = dBNameToDataValue;
        Key = key;
    }

    public string Key { get; set; }

    /// <summary>
    ///     设备名称
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    ///     变量和值的对应
    /// </summary>
    public Dictionary<string, string> DbNameToDataValue { get; }


    /// <summary>
    ///     是否使用缓存
    ///     这样可以减少对数据库的访问
    /// </summary>
    public bool UseMemory { get; set; } = true;

    public void Insert(string dbName, string value)
    {
        DbNameToDataValue[dbName] = value;
    }
}