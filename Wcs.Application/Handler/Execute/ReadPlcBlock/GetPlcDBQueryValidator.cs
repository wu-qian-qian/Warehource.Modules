using FluentValidation;

namespace Wcs.Application.Handler.Execute.ReadPlcBlock;

internal class GetPlcDBQueryValidator : AbstractValidator<GetPlcDBQuery>
{
    public GetPlcDBQueryValidator()
    {
        RuleFor(p => p.DeviceName).NotEmpty();
    }
}