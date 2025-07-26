using Common.Application.MediatR.Message;
using Identity.Contrancts;

namespace Identity.Application.AddHandler;

public sealed record AddRoleEvent : ICommand<RoleDto>
{
    public string RoleName { get; set; }
    public string Description { get; set; }
}