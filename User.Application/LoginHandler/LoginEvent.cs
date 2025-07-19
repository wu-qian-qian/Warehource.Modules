using Common.Application.MediatR.Message;
using User.Contrancts;

namespace User.Application.LoginHandler;

internal sealed record LoginEvent : ICommand<UserDto>
{
    public string Username { get; set; }

    public string Password { get; set; }
}