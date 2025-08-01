using Common.Application.MediatR.Message;
using Identity.Contrancts;

namespace Identity.Application.Handler.Login;

public sealed record LoginEvent : ICommand<UserDto>
{
    public string Username { get; set; }

    public string Password { get; set; }
}