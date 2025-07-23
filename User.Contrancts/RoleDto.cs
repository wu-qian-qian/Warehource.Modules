namespace Identity.Contrancts;

public class RoleDto
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }
    public string? Description { get; set; }
}