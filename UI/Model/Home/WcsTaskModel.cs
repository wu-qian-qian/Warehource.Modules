namespace UI.Model.Home;

public class WcsTaskModel
{
    public string TaskCode { get; set; }

    /// <summary>
    ///     流水号
    /// </summary>
    public int SerialNumber { get; set; }

    /// <summary>
    ///     任务类型
    /// </summary>
    public string TaskTypeDesc { get; set; }

    /// <summary>
    ///     任务状态
    /// </summary>
    public string TaskStatusDesc { get; set; }

    /// <summary>
    ///     任务描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     取巷道
    /// </summary>
    public int? GetTunnel { get; set; }

    /// <summary>
    ///     取层
    /// </summary>
    public int? GetFloor { get; set; }

    /// <summary>
    ///     取排
    /// </summary>
    public int? GetRow { get; set; }

    /// <summary>
    ///     取列
    /// </summary>
    public int? GetColumn { get; set; }

    /// <summary>
    ///     取深度
    /// </summary>
    public int? GetDepth { get; set; }


    /// <summary>
    ///     放巷道
    /// </summary>
    public int? PutTunnel { get; set; }

    /// <summary>
    ///     放层
    /// </summary>
    public int? PutFloor { get; set; }

    /// <summary>
    ///     放排
    /// </summary>
    public int? PutRow { get; set; }

    /// <summary>
    ///     放列
    /// </summary>
    public int? PutColumn { get; set; }

    /// <summary>
    ///     放深度
    /// </summary>
    public int? PutDepth { get; set; }

    /// <summary>
    ///     步骤
    /// </summary>
    public string ExecuteDesc { get; set; }

    public DateTime CreationTime { get; set; }
}