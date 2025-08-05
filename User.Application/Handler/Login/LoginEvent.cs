using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Identity.Contrancts;

namespace Identity.Application.Handler.Login;

public sealed record LoginEvent : ICommand<Result<string>>
{
    public string Username { get; set; }

    public string Password { get; set; }
}