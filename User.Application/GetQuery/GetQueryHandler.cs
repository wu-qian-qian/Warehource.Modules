using Common.Application.MediatR.Message;
using User.Contrancts;

namespace User.Application.GetQuery;

internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, IEnumerable<UserDto>>
{
    public Task<IEnumerable<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

internal class GetRoleQueryHandler : IQueryHandler<GetRoleQuery, IEnumerable<RoleDto>>
{
    public Task<IEnumerable<RoleDto>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}