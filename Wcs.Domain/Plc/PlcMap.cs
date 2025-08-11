using Common.Domain.EF;

namespace Wcs.Domain.Plc;

/// <summary>
///     与PLC进行映射的表格
/// </summary>
public class PlcMap : IEntity
{
    public PlcMap() : base(Guid.NewGuid())
    {
    }

    public string DeviceName { get; set; }

    public string? Description { get; set; }

    public string PlcName { get; set; }
}