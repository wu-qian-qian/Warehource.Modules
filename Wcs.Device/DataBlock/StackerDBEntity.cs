using Wcs.Device.Abstract;

namespace Wcs.Device.DataBlock;

public class StackerDBEntity : BaseDBEntity
{
    public string RTask { get; set; }

    public string RFloor { get; set; }

    public string RRow { get; set; }

    public string RColumn { get; set; }

    /// <summary>
    ///     1 手动
    ///     2 自动
    /// </summary>
    public string RMode { get; set; }

    public string RLoad { get; set; }

    public string RErrCode { get; set; }

    /// <summary>
    ///     0 初始化
    ///     1 可下发
    ///     2 任务完成
    /// </summary>
    public string RExecuteStatus { get; set; }

    public string WTask { get; set; }

    public string WGetFloor { get; set; }

    public string WGetRow { get; set; }

    public string WGetColumn { get; set; }

    public string WPutFloor { get; set; }

    public string WPutRow { get; set; }

    public string WPutColumn { get; set; }

    public string WStart { get; set; }

    public string WTaskType { get; set; }
}