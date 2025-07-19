using FluentValidation;

namespace User.Application.GetQuery;

internal class GetUserQueryValidation : AbstractValidator<GetRoleQuery>
{
}

internal class GetRoleQueryValidation : AbstractValidator<GetRoleQuery>
{
}