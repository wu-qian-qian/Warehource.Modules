using Common.Domain.Event;

namespace Plc.CustomEvents;

public class S7WritePlcDataBlockEvent : IMassTransitDomainEvent
{
    public S7WritePlcDataBlockEvent(DateTime occurredOnUtc) : base(occurredOnUtc)
    {
    }

    /// <summary>
    ///     设备名称
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    ///     变量和值的对应
    /// </summary>
    public Dictionary<string, string> DBNameToDataValue { get; }

    public string Ip { get; set; }

    /// <summary>
    ///     是否使用缓存
    ///     这样可以减少对数据库的访问
    /// </summary>
    public bool UseMemory { get; set; } = true;

    public void Inser(string dbName, string value)
    {
        DBNameToDataValue[dbName] = value;
    }
}