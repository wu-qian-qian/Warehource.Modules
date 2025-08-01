using FluentValidation;

namespace Plc.Application.PlcHandler.Write;

///
internal class WritePlcEventCommandValidator : AbstractValidator<WritePlcEventCommand>
{
    public WritePlcEventCommandValidator()
    {
        RuleFor(model => model)
            .Must(model =>
                model.DeviceName != null)
            .WithMessage("必须都存在才能进行写入");
    }
}