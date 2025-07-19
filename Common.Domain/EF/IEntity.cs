namespace Common.Domain.EF;

public abstract class IEntity : ISoftDelete, ILastModification
{
    public IEntity(Guid id)
    {
        Id = id;
        IsDeleted = false;
        CreationTime = DateTime.Now;
    }

    public Guid Id { get; init; }

    public DateTime CreationTime { get; private set; }

    public string? LastModifierUser { get; private set; }

    public DateTime? LastModificationTime { get; private set; }

    public void SetLastModification(string user)
    {
        LastModifierUser = user ?? throw new ArgumentNullException(nameof(user));
        LastModificationTime = DateTime.Now;
    }

    public bool IsDeleted { get; private set; }

    public void SoftDelete()
    {
        IsDeleted = true;
    }
}