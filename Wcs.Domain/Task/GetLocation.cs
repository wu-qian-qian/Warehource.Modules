namespace Wcs.Domain.Task;

public record GetLocation(
    string? GetTunnel,
    string? GetFloor,
    string? GetRow,
    string? GetColumn,
    string? GetDepth
)
{
    public bool IsValid()
    {
        return !string.IsNullOrEmpty(GetTunnel)
               && !string.IsNullOrEmpty(GetFloor)
               && !string.IsNullOrEmpty(GetRow)
               && !string.IsNullOrEmpty(GetColumn)
               && !string.IsNullOrEmpty(GetDepth);
    }
}