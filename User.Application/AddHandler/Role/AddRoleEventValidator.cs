using FluentValidation;

namespace Identity.Application.AddHandler;

internal class AddRoleEventValidator : AbstractValidator<AddRoleEvent>
{
    public AddRoleEventValidator()
    {
        RuleFor(p => p.Description).Length(0, 20);
        RuleFor(p => p.RoleName).NotNull().Length(1, 20);
    }
}