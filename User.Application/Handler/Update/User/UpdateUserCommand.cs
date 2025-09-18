using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Identity.Contrancts.Respon;

namespace Identity.Application.Handler.Update.User;

public sealed record UpdateUserCommand : ICommand<Result<UserDto>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string? Description { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public bool LockoutEnd { get; set; }

    public string RoleName { get; set; }
}