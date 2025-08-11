using FluentValidation;

namespace Wcs.Application.ExecuteDevice.ReadPlcBlock;

internal class GetPlcDBtValidator : AbstractValidator<GetPlcDBQuery>
{
    public GetPlcDBtValidator()
    {
        RuleFor(p => p.DeviceName).NotEmpty();
    }
}