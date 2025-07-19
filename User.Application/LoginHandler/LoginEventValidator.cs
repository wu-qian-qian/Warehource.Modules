using FluentValidation;

namespace User.Application.LoginHandler;

internal class LoginEventValidator : AbstractValidator<LoginEvent>
{
    public LoginEventValidator()
    {
        RuleFor(p => p.Username).NotNull().Length(8, 20);
        RuleFor(x => x.Password).NotNull().Length(8, 20);
    }
}