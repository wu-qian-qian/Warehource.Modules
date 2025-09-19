namespace Identity.Contrancts.Request.Update;

public class UpdateUserRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string RoleName { get; set; }

    public bool LockoutEnd { get; set; }
}