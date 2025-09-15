using Common.Application.MediatR.Message;
using Identity.Contrancts.Respon;

namespace Identity.Application.Handler.Get.Role;

public class GetRoleQuery : IQuery<IEnumerable<RoleDto>>
{
}