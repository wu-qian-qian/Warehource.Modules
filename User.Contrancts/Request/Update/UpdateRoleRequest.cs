namespace Identity.Contrancts.Request.Update;

public class UpdateRoleRequest
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
}