using Common.Application.MediatR.Message;
using User.Contrancts;

namespace User.Application.AddHandler;

public sealed record AddUserEvent : ICommand<UserDto>
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public DateTimeOffset LockoutEnd { get; set; }

    public string RoleName { get; set; }
}

public sealed record AddRoleEvent : ICommand<RoleDto>
{
    public string RoleName { get; set; }
    public string Description { get; set; }
}