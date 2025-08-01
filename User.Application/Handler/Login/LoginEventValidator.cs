using FluentValidation;

namespace Identity.Application.Handler.Login;

internal class LoginEventValidator : AbstractValidator<LoginEvent>
{
    public LoginEventValidator()
    {
        RuleFor(p => p.Username).NotNull().Length(1, 20);
        RuleFor(x => x.Password).NotNull().Length(1, 20);
    }
}