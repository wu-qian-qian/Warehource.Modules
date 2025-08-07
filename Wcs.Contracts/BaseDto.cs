namespace Wcs.Contracts;

public class BaseDto
{
    public Guid Id { get; set; }

    public DateTime CreationTime { get; set;}

    public string? LastModifierUser { get; set;}

    public DateTime? LastModificationTime { get; set;}
}