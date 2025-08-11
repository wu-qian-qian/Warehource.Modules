using FluentValidation;

namespace Wcs.Application.DBHandler.PlcMap.InsertOrUpdate;

internal class InsertOrUpdateValibator : AbstractValidator<InsertOrUpdateEvent>
{
    public InsertOrUpdateValibator()
    {
        RuleFor(p => p.DeviceName).MaximumLength(28).NotEmpty().NotNull();
    }
}