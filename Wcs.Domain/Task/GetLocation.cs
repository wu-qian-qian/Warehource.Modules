namespace Wcs.Domain.Task;

public record GetLocation(
    string? GetTunnel,
    string? GetFloor,
    string? GetRow,
    string? GetColumn,
    string? GetDepth
);