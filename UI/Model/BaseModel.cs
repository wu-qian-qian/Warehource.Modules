namespace UI.Model;

public class BaseModel
{
    public Guid Id { get; set; }

    public DateTime CreationTime { get; set; }

    public string? LastModifierUser { get; set; }

    public DateTime? LastModificationTime { get; set; }
}