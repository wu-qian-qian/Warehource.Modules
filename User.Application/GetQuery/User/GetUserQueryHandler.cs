using AutoMapper;
using Common.Application.MediatR.Message;
using Identity.Contrancts;
using Identity.Domain;

namespace Identity.Application.GetQuery.User;

internal class GetUserQueryHandler(UserManager userManager, IMapper mapper)
    : IQueryHandler<GetUserQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var userList = await userManager.GetAllUserAndRoleAsync();
        return mapper.Map<IEnumerable<UserDto>>(userList);
    }
}