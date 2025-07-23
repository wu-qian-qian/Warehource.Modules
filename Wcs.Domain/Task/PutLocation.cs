namespace Wcs.Domain.Task;

public record class PutLocation(
    string? PutTunnel,
    string? PutFloor,
    string? PutRow,
    string? PutColumn,
    string? PutDepth
);