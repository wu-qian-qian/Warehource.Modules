using FluentValidation;

namespace Wcs.Application.Handler.DB.Job.SetStatus;

internal class StatusJobCommandValidator : AbstractValidator<StatusJobCommand>
{
    public StatusJobCommandValidator()
    {
        RuleFor(p => p.Name).NotNull().Length(1, 50);
    }
}