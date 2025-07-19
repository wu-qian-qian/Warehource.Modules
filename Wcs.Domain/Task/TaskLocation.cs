namespace Wcs.Domain.Task;

public record GetLocation(
    string? GetTunnel,
    string? GetFloor,
    string? GetRow,
    string? GetColumn,
    string? GetDepth
);

public record class PutLocation(
    string? PutTunnel,
    string? PutFloor,
    string? PutRow,
    string? PutColumn,
    string? PutDepth
);