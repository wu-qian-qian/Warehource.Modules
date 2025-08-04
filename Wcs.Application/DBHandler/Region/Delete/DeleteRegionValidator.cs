using FluentValidation;

namespace Wcs.Application.DBHandler.Region.Delete;

public class DeleteRegionValidator : AbstractValidator<DeleteRegionEvent>
{
    public DeleteRegionValidator()
    {
        RuleFor(p => p.Id).NotNull();
    }
}