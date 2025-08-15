using FluentValidation;

namespace Wcs.Application.Handler.DataBase.Region.AddOrUpdate;

public class AddOrUpdateRegionCommandValidator : AbstractValidator<AddOrUpdateRegionCommand>
{
    public AddOrUpdateRegionCommandValidator()
    {
        RuleFor(p => p.Code).NotEmpty().NotNull();
    }
}