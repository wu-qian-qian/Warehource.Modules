namespace Identity.Presentation.Add;

internal sealed class AddUserRequst
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string RoleName { get; set; }
}