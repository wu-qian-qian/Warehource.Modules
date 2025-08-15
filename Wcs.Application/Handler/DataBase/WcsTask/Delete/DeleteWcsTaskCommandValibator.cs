using FluentValidation;

namespace Wcs.Application.Handler.DataBase.WcsTask.Delete;

public class DeleteWcsTaskCommandValibator : AbstractValidator<DeleteWcsTaskCommand>
{
    public DeleteWcsTaskCommandValibator()
    {
        RuleFor(p => p)
            .Must(p => p.SerialNumber != null || p.TaskCode != null).WithMessage("必须有一个标识不能为空");
    }
}