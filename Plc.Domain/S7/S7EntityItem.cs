using Common.Domain.EF;
using Plc.Shared;

namespace Plc.Domain.S7;

/// <summary>
///     变量实体类
/// </summary>
public class S7EntityItem : IEntity
{
    public S7EntityItem() : base(Guid.NewGuid())
    {
    }

    public string Ip { get; set; }

    /// <summary>
    ///     PLC的的数据类型
    /// </summary>
    public S7DataTypeEnum S7DataType { get; set; }


    /// <summary>
    ///     db地址
    /// </summary>
    public int DBAddress { get; set; }

    /// <summary>
    ///     PLC的byte的偏移量
    /// </summary>
    public int DataOffset { get; set; }

    /// <summary>
    ///     bit的偏移量
    /// </summary>
    public byte? BitOffset { get; set; }

    /// <summary>
    ///     PLC的存储类型
    /// </summary>
    public S7BlockTypeEnum S7BlockType { get; set; }

    /// <summary>
    ///     PLC索引
    /// </summary>
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

    public bool IsUse { get; set; }

    /// <summary>
    ///     网络配置
    /// </summary>
    public Guid NetGuid { get; set; }

    ///// <summary>
    ///// 需要配置索引
    ///// 唯一编码防止重复添加
    ///// </summary>
    //public int HashCode { get; set; }

    //public void CreateCode()
    //{
    //    var code = $"{S7DataType}{DBAddress}{DataOffset}{BitOffset}{S7BlockType}{ArrayLength}";
    //    HashCode = code.GetHashCode();
    //}
}