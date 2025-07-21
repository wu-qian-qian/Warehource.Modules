using Common.Application.MediatR.Message;
using User.Contrancts;

namespace User.Application.GetQuery;

public class GetUserQuery : IQuery<IEnumerable<UserDto>>
{
}

public class GetRoleQuery : IQuery<IEnumerable<RoleDto>>
{
}