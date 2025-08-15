using FluentValidation;

namespace Wcs.Application.Handler.DataBase.ExecueNode.AddOrUpdate;

public class AddOrUpdateExecuteNodeCommandValibator : AbstractValidator<AddOrUpdateExecuteNodeCommand>
{
    public AddOrUpdateExecuteNodeCommandValibator()
    {
        RuleFor(x => x.PahtNodeGroup).NotEmpty().WithMessage("Name不能为空");
    }
}