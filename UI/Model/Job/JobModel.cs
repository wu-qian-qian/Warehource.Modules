namespace UI.Model.Job;

public class JobModel
{
    public string Name { get; set; }

    /// <summary>
    ///     描述，用来标识任务属于哪个类型执行
    /// </summary>
    public string? Description { get; set; }

    public string JobType { get; set; }

    public int TimeOut { get; set; }

    public int Timer { get; set; }

    public bool IsStart { get; set; }

    public DateTime CreationTime { get; set; }
}