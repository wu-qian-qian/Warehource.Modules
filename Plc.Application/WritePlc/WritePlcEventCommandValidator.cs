using FluentValidation;

namespace Plc.Application.ReadPlc;

///
internal class WritePlcEventCommandValidator : AbstractValidator<WritePlcEventCommand>
{
    public WritePlcEventCommandValidator()
    {
        RuleFor(model => model)
            .Must(model =>
                model.Ip != null &&
                model.DeviceName != null)
            .WithMessage("必须都存在才能进行写入");
    }
}