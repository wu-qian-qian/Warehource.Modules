using AutoMapper;
using Common.Application.MediatR.Message;
using Identity.Contrancts;
using Identity.Domain;

namespace Identity.Application.Handler.Get.Role;

internal class GetRoleQueryHandler(UserManager userManager, IMapper mapper)
    : IQueryHandler<GetRoleQuery, IEnumerable<RoleDto>>
{
    public async Task<IEnumerable<RoleDto>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var userList = await userManager.GetRolesAsync();
        return mapper.Map<IEnumerable<RoleDto>>(userList);
    }
}