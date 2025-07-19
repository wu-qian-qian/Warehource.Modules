using FluentValidation;

namespace Wcs.Application.JobHandler.DeleteCommand;

internal class DeleteJobEventValidator : AbstractValidator<DeleteJobEvent>
{
    public DeleteJobEventValidator()
    {
        RuleFor(p => p.Name).NotNull().Length(1, 50);
    }
}