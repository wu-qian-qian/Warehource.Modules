namespace Wcs.Contracts;

public class BaseDto
{
    public Guid Id { get; init; }

    public DateTime CreationTime { get; }

    public string? LastModifierUser { get; }

    public DateTime? LastModificationTime { get; }
}