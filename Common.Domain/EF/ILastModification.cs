namespace Common.Domain.EF;

public interface ILastModification
{
    string? LastModifierUser { get; } //最后修改人

    DateTime? LastModificationTime { get; } //最后修改时间

    void SetLastModification(string user);
}