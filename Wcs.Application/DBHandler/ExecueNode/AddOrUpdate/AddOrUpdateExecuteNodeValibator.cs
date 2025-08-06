using FluentValidation;

namespace Wcs.Application.DBHandler.ExecueNode.AddOrUpdate;

public class AddOrUpdateExecuteNodeValibator : AbstractValidator<AddOrUpdateExecuteNodeEvent>
{
    public AddOrUpdateExecuteNodeValibator()
    {
        RuleFor(x => x.PahtNodeGroup).NotEmpty().WithMessage("Name不能为空");
        RuleFor(x => x.CurrentDeviceName).NotEmpty().WithMessage("设备名不能位空");
        RuleFor(x => x.RegionCode).NotEmpty().WithMessage("区域编码不能为空");
    }
}