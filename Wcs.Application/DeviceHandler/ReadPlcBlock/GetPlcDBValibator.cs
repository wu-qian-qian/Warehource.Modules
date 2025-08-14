using FluentValidation;

namespace Wcs.Application.DeviceHandler.ReadPlcBlock;

internal class GetPlcDBtValidator : AbstractValidator<GetPlcDBQuery>
{
    public GetPlcDBtValidator()
    {
        RuleFor(p => p.DeviceName).NotEmpty();
    }
}