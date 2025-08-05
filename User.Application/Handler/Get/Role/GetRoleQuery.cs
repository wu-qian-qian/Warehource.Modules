using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Identity.Contrancts;

namespace Identity.Application.Handler.Get.Role;

public class GetRoleQuery : IQuery<IEnumerable<RoleDto>>
{
}