using FluentValidation;

namespace Wcs.Application.DBHandler.Device.AddOrUpdate;

public class AddOrUpdateDeviceValibator : AbstractValidator<AddOrUpdateDeviceEvent>
{
    public AddOrUpdateDeviceValibator()
    {
        RuleFor(x => x.DeviceName).NotEmpty().WithMessage("Name不能为空");
        RuleFor(x => x.Config).NotEmpty().WithMessage("配置不能为空");
    }
}