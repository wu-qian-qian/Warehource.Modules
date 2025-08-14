using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;

namespace Identity.Application.Handler.Login;

public sealed record LoginCommand : ICommand<Result<string>>
{
    public string Username { get; set; }

    public string Password { get; set; }
}