using FluentValidation;

namespace Plc.Application.S7Plc.Insert;

public class InsertS7NetConfigValidator : AbstractValidator<InsertS7NetConfigCommand>
{
    public InsertS7NetConfigValidator()
    {
        RuleFor(p => p.S7NetRequests).NotNull();
        RuleFor(p => p.S7NetEntityItemRequests).NotNull();
    }
}