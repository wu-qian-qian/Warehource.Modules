namespace Wcs.Contracts.Request.Region;

public record RegionRequest(Guid? Id, string Code, string? Description);