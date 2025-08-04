namespace UI.Model.Plc;

public class S7NetModel
{
    public string Ip { get; set; }

    /// <summary>
    ///     端口
    /// </summary>
    public int Port { get; set; }

    public string S7Type { get; set; }

    /// <summary>
    ///     槽号
    /// </summary>
    public short Solt { get; set; }

    /// <summary>
    ///     机架
    /// </summary>
    public short Rack { get; set; }

    /// <summary>
    ///     读取超时
    /// </summary>
    public int ReadTimeOut { get; set; }

    /// <summary>
    ///     写入超时
    /// </summary>
    public int WriteTimeOut { get; set; }

    public S7EntityItemModel[] s7EntityItemModels { get; set; }
}