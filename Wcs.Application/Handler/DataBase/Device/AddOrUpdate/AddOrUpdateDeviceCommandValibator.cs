using FluentValidation;

namespace Wcs.Application.Handler.DataBase.Device.AddOrUpdate;

public class AddOrUpdateDeviceCommandValibator : AbstractValidator<AddOrUpdateDeviceCommand>
{
    public AddOrUpdateDeviceCommandValibator()
    {
        RuleFor(x => x.DeviceName).NotEmpty().WithMessage("Name不能为空");
        RuleFor(x => x.Config).NotEmpty().WithMessage("配置不能为空");
    }
}