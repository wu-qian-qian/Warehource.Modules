using AutoMapper;
using Common.Application.MediatR.Message;
using Identity.Contrancts.Respon;
using Identity.Domain;

namespace Identity.Application.Handler.Get.User;

internal class GetUserQueryHandler(UserManager userManager, IMapper mapper)
    : IQueryHandler<GetUserQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var userList = await userManager.GetAllUserAndRoleAsync();
        if (request.UserName != string.Empty) userList = userList.Where(p => p.Username == request.UserName);

        return mapper.Map<IEnumerable<UserDto>>(userList);
    }
}