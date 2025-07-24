namespace Identity.Contrancts.Request;

public record AddRoleRequest
{
    public string RoleName { get; set; }
    public string Description { get; set; }
}