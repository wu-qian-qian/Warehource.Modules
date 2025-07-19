using FluentValidation;

namespace User.Application.AddHandler;

internal class AddUserEventValidator : AbstractValidator<AddUserEvent>
{
    public AddUserEventValidator()
    {
        RuleFor(p => p.Name).NotNull().Length(8, 20);
        RuleFor(p => p.Description).Length(0, 20);
        RuleFor(p => p.RoleName).NotNull().Length(1, 20);
        RuleFor(p => p.Username).NotNull().Length(8, 20);
        RuleFor(x => x.Password)
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d).+$")
            .WithMessage("密码必须同时包含字母和数字").Length(8, 20);
    }
}

internal class AddRoleEventValidator : AbstractValidator<AddRoleEvent>
{
    public AddRoleEventValidator()
    {
        RuleFor(p => p.Description).Length(0, 20);
        RuleFor(p => p.RoleName).NotNull().Length(1, 20);
    }
}