namespace Wcs.Contracts.Respon.WcsTask;

public class WcsTaskDto : BaseDto
{
    public int Level { get; set; }

    public bool IsEnforce { get; set; }
    public string TaskCode { get; set; }

    /// <summary>
    ///     流水号
    /// </summary>
    public int SerialNumber { get; set; }

    /// <summary>
    ///     容器编码
    /// </summary>
    public string Container { get; set; }

    /// <summary>
    ///     任务类型
    /// </summary>
    public string TaskType { get; set; }

    /// <summary>
    ///     任务状态
    /// </summary>
    public string TaskStatus { get; set; }

    /// <summary>
    ///     创建系统
    /// </summary>
    public string CreatorSystemType { get; set; }

    /// <summary>
    ///     任务描述
    /// </summary>
    public string? Description { get; set; }

    public string? GetLocation { get; set; }

    public string? PutLocation { get; set; }

    /// <summary>
    ///     步骤
    /// </summary>
    public string? ExecuteDesc { get; set; }

    /// <summary>
    ///     路径
    /// </summary>
    public string? ExecutePath { get; set; }

    public string? StockInPosition { get; set; }


    public string? StockOutPosition { get; set; }

    public string? CurrentDevice { get; set; }
}