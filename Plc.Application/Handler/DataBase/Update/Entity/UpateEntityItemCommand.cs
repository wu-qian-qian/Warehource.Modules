using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Plc.Shared;

namespace Plc.Application.Handler.DataBase.Update.Entity;

public class UpateEntityItemCommand : ICommand<Result<bool>>
{
    public Guid Id { get; set; }
    public S7DataTypeEnum S7DataType { get; set; }

    public S7BlockTypeEnum S7BlockType { get; set; }

    public bool IsUse { get; set; }

    public int DBAddress { get; set; }

    /// <summary>
    ///     PLC的byte的偏移量
    /// </summary>
    public int DataOffset { get; set; }

    /// <summary>
    ///     bit的偏移量
    /// </summary>
    public byte? BitOffset { get; set; }

    public int Index { get; set; }

    /// <summary>
    ///     字段名称
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     字段名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     数组类型的长度
    /// </summary>
    public byte? ArrayLength { get; set; }

    /// <summary>
    ///     关联设备
    /// </summary>
    public string DeviceName { get; set; }
}