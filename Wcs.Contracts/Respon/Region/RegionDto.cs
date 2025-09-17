namespace Wcs.Contracts.Respon.Region;

public class RegionDto : BaseDto
{
    public string Code { get; set; }

    public string? Description { get; set; }

    public int MaxNum { get; set; }

    public int CurrentNum { get; set; }
}