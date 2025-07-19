using Common.Domain.EF;

namespace User.Domain;

public class Role : IEntity
{
    public Role() : base(Guid.NewGuid())
    {
    }

    public string RoleName { get; set; }
    public string? Description { get; set; }
}