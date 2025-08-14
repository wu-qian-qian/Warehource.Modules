using FluentValidation;

namespace Identity.Application.Handler.Add.Role;

internal class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
{
    public AddRoleCommandValidator()
    {
        RuleFor(p => p.Description).Length(0, 20);
        RuleFor(p => p.RoleName).NotNull().Length(1, 20);
    }
}