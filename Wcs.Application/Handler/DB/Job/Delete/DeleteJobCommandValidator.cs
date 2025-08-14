using FluentValidation;

namespace Wcs.Application.Handler.DB.Job.Delete;

internal class DeleteJobCommandValidator : AbstractValidator<DeleteJobCommand>
{
    public DeleteJobCommandValidator()
    {
        RuleFor(p => p.Name).NotNull().Length(1, 50);
    }
}