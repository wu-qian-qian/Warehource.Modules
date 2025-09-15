using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Identity.Contrancts.Respon;

namespace Identity.Application.Handler.Add.Role;

public sealed record AddRoleCommand : ICommand<Result<RoleDto>>
{
    public string RoleName { get; set; }
    public string Description { get; set; }
}