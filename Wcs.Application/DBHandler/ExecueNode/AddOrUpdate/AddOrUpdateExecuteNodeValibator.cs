using FluentValidation;

namespace Wcs.Application.DBHandler.ExecueNode.AddOrUpdate;

public class AddOrUpdateExecuteNodeValibator : AbstractValidator<AddOrUpdateExecuteNodeEvent>
{
    public AddOrUpdateExecuteNodeValibator()
    {
        RuleFor(x => x.PahtNodeGroup).NotEmpty().WithMessage("Name不能为空");
    }
}