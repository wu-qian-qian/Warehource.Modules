using AutoMapper;
using Common.Application.MediatR.Message;
using User.Contrancts;
using User.Domain;

namespace User.Application.GetQuery;

internal class GetUserQueryHandler(UserManager userManager, IMapper mapper)
    : IQueryHandler<GetUserQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var userList = await userManager.GetAllUserAndRoleAsync();
        return mapper.Map<IEnumerable<UserDto>>(userList);
    }
}

internal class GetRoleQueryHandler(UserManager userManager, IMapper mapper)
    : IQueryHandler<GetRoleQuery, IEnumerable<RoleDto>>
{
    public async Task<IEnumerable<RoleDto>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var userList = await userManager.GetRolesAsync();
        return mapper.Map<IEnumerable<RoleDto>>(userList);
    }
}