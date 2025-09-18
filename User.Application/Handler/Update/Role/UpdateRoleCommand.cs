using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Identity.Contrancts.Respon;

namespace Identity.Application.Handler.Update.Role;

public sealed record UpdateRoleCommand : ICommand<Result<RoleDto>>
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
}