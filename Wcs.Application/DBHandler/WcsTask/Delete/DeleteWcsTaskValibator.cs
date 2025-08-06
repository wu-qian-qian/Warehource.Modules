using FluentValidation;

namespace Wcs.Application.DBHandler.WcsTask.Delete;

public class DeleteWcsTaskValibator : AbstractValidator<DeleteWcsTaskEvent>
{
    public DeleteWcsTaskValibator()
    {
        RuleFor(p => p)
            .Must(p => p.SerialNumber != null || p.TaskCode != null).WithMessage("必须有一个标识不能为空");
    }
}