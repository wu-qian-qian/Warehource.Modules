using FluentValidation;

namespace Wcs.Application.Handler.DataBase.Job.Update;

internal class UpdateJobCommandValidator : AbstractValidator<UpdateJobCommand>
{
    public UpdateJobCommandValidator()
    {
        RuleFor(p => p.Name).NotNull().Length(1, 50);
    }
}