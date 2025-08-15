using FluentValidation;

namespace Wcs.Application.Handler.DataBase.Job.Insert;

internal class AddJobCommandValidator : AbstractValidator<AddJobCommand>
{
    public AddJobCommandValidator()
    {
        RuleFor(p => p.Name).NotNull().Length(1, 50);
        RuleFor(p => p.JobType).NotNull().Length(1, 50);
    }
}