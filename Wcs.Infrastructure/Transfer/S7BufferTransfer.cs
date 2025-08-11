namespace Wcs.Infrastructure.Transfer;

/// <summary>
///     整改，plc应该只用来做通讯，而不应该有表的产生
///     因为表数据与wcs有关系
///     所以需要隔离
/// </summary>
internal class S7BufferTransfer
{
    public string Transfer(byte[] buffer)
    {
        return string.Empty;
    }
}