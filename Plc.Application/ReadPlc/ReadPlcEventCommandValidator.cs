﻿using FluentValidation;

namespace Plc.Application.ReadPlc;

///
internal class ReadPlcEventCommandValidator : AbstractValidator<ReadPlcEventCommand>
{
    public ReadPlcEventCommandValidator()
    {
        RuleFor(model => model)
            .Must(model =>
                model.Ip != null ||
                model.DeviceName != null)
            .WithMessage("ip和设备编号必须存在一个");
    }
}