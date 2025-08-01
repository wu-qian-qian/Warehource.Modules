using FluentValidation;

namespace Wcs.Application.DBHandler.Job.SetStatus;

internal class StatusJobEventValidator : AbstractValidator<StatusJobEvent>
{
    public StatusJobEventValidator()
    {
        RuleFor(p => p.Name).NotNull().Length(1, 50);
    }
}