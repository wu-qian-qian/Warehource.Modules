namespace Wcs.Contracts.Options;

/// <summary>
/// 用于巷道获取执行的设备  ，该处理占时只考虑一轨道单机设备
/// </summary>
public class StackerMapOptions
{
    public StackerMapOptions()
    {
    }

    public List<TunnleMapStacker> StackerMap { get; set; }
}

public class TunnleMapStacker
{
    public int Tunnle { get; set; }

    public string Stacker { get; set; }
}