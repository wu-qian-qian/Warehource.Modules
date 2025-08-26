using Wcs.Device.Abstract;

namespace Wcs.Device.DataBlock;

public class PipeLineDBEntity : BaseDBEntity
{
    public string RTask { get; set; }

    public string RBarCode { get; set; }

    /// <summary>
    ///     1在线
    ///     0手动
    /// </summary>
    public string RMode { get; set; }

    public string RFree { get; set; }

    public string RErrCode { get; set; }

    public string RLoad { get; set; }

    public string WTask { get; set; }

    public string WTargetCode { get; set; }

    public string WStart { get; set; }

    public string WTaskType { get; set; }
}