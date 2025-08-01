using FluentValidation;

namespace Wcs.Application.DBHandler.Job.Get;

internal class GetJobQueryValidator : AbstractValidator<GetJobQuery>
{
    public GetJobQueryValidator()
    {
        RuleFor(p => p.Id).NotNull();
    }
}