using FluentValidation;

namespace Identity.Application.AddHandler.User;

internal class AddUserEventValidator : AbstractValidator<AddUserEvent>
{
    public AddUserEventValidator()
    {
        RuleFor(p => p.Name).NotNull().Length(1, 20);
        RuleFor(p => p.Description).Length(0, 20);
        RuleFor(p => p.RoleName).NotNull().Length(1, 20);
        RuleFor(p => p.Username).NotNull().Length(5, 20);
        RuleFor(x => x.Password);

        //  .WithMessage("密码必须同时包含字母和数字").Length(3, 20);
    }
}