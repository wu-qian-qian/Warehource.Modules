namespace Wcs.Domain.Task;

public record class PutLocation(
    string? PutTunnel,
    string? PutFloor,
    string? PutRow,
    string? PutColumn,
    string? PutDepth
)
{
    public bool IsValid()
    {
        return !string.IsNullOrEmpty(PutTunnel)
               && !string.IsNullOrEmpty(PutFloor)
               && !string.IsNullOrEmpty(PutRow)
               && !string.IsNullOrEmpty(PutColumn)
               && !string.IsNullOrEmpty(PutDepth);
    }
}