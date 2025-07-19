using FluentValidation;

namespace Wcs.Application.JobHandler.AddCommand;

internal class AddJobEventValidator : AbstractValidator<AddJobEvent>
{
    public AddJobEventValidator()
    {
        RuleFor(p => p.Name).NotNull().Length(1, 50);
        RuleFor(p => p.JobType).NotNull().Length(1, 50);
    }
}