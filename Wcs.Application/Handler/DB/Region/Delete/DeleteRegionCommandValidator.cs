using FluentValidation;

namespace Wcs.Application.Handler.DB.Region.Delete;

public class DeleteRegionCommandValidator : AbstractValidator<DeleteRegionCommand>
{
    public DeleteRegionCommandValidator()
    {
        RuleFor(p => p.Id).NotNull();
    }
}