using Common.Domain.EF;

namespace Wcs.Domain.Map;

/// <summary>
///     区域映射表，
/// </summary>
public class RegionMap : IEntity
{
    public RegionMap() : base(Guid.NewGuid())
    {
    }

    /// <summary>
    ///     起点位置  或者  终点位置
    /// </summary>
    public string Code { get; set; }

    public string RegionCode { get; set; }
    public string Description { get; set; }
}