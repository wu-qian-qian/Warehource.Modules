using FluentValidation;

namespace Wcs.Application.Handler.DB.PlcMap.InsertOrUpdate;

internal class InsertOrUpdateCommandValibator : AbstractValidator<InsertOrUpdateCommand>
{
    public InsertOrUpdateCommandValibator()
    {
        RuleFor(p => p.DeviceName).MaximumLength(28).NotEmpty().NotNull();
    }
}