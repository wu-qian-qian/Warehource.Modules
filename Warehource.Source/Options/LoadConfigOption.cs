namespace Warehource.Source.Options;

public class LoadConfigOption
{
    public bool LoadDataBase { get; set; }

    /// <summary>
    ///     2种方式加载job，api与配置
    /// </summary>
    public bool LoadJob { get; set; }
}