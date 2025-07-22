using AutoMapper;
using Common.Application.Exception;
using Common.Application.MediatR.Message;
using User.Contrancts;
using User.Domain;

namespace User.Application.LoginHandler;

internal class LoginEventHandler(UserManager userManager, IMapper mapper) : ICommandHandler<LoginEvent, UserDto>
{
    public async Task<UserDto> Handle(LoginEvent request, CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserAndRoleAsync(request.Username);
        if (user != null && user.CheckLockoutEnd() && user.CheckLogin(request.Password))
            return mapper.Map<UserDto>(user);
        throw new CommonException("用户不存在");
    }
}