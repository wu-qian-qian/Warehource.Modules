namespace UI.Model.Identity;

public class IdentityUserModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Username { get; set; }

    public string PassWord { get; set; }

    public DateTimeOffset LockoutEnd { get; set; }

    public string RoleName { get; set; }
}