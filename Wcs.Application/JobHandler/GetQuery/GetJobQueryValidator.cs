using FluentValidation;

namespace Wcs.Application.JobHandler.GetQuery;

internal class GetJobQueryValidator : AbstractValidator<GetJobQuery>
{
    public GetJobQueryValidator()
    {
        RuleFor(p => p.Id).NotNull();
    }
}