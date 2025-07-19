using Common.Application.MediatR.Message;
using User.Contrancts;

namespace User.Application.LoginHandler;

internal class LoginEventHandler : ICommandHandler<LoginEvent, UserDto>
{
    public Task<UserDto> Handle(LoginEvent request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}