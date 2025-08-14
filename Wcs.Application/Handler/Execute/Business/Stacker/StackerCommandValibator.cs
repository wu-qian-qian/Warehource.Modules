using FluentValidation;

namespace Wcs.Application.Handler.Execute.Business.Stacker;

internal class StackerCommandValibator : AbstractValidator<StackerCommand>
{
    public StackerCommandValibator()
    {
        RuleFor(p => p.Stacker).NotNull();
    }
}