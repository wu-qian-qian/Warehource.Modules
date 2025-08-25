using Wcs.Device.Abstract;

namespace Wcs.Device.DataBlock;

public class PipeLineDBEntity : BaseDBEntity
{
    public string RTask { get; set; }

    public string RBarCode { get; set; }

    public string WTask { get; set; }

    public string WTargetCode { get; set; }
}