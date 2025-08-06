using Common.Application.Encodings;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Identity.Domain;

namespace Identity.Application.Handler.Login;

internal class LoginEventHandler(UserManager userManager, ITokenService tokenService)
    : ICommandHandler<LoginEvent, Result<string>>
{
    public async Task<Result<string>> Handle(LoginEvent request, CancellationToken cancellationToken)
    {
        var result = new Result<string>();
        var user = await userManager.GetUserAndRoleAsync(request.Username);
        if (user != null && user.CheckLockoutEnd() && user.CheckLogin(request.Password))
        {
            result.IsSuccess = true;
            result.Value = tokenService.BuildJwtString([user.Role.RoleName], [user.Name]);
        }
        else
        {
            result.IsSuccess = false;
            result.Message = $"无该用户{request.Username}";
        }

        return result;
    }
}