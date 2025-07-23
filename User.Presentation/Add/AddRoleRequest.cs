namespace Identity.Presentation.Add;

public record AddRoleRequest
{
    public string RoleName { get; set; }
    public string Description { get; set; }
}