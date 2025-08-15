using FluentValidation;

namespace Wcs.Application.Handler.DataBase.WcsTask.AddOrUpdate;

public class AddOrUpdateWcsTaskCommandValibator : AbstractValidator<AddOrUpdateWcsTaskCommand>
{
    public AddOrUpdateWcsTaskCommandValibator()
    {
        // RuleFor(model => model)
        //     .Must(model => 
        //         (model.GetColumn!=null&&model.GetFloor!=null&&model.GetRow!=null)||
        //         (model.PutColumn!=null&&model.PutFloor!=null&&model.PutRow!=null))
        //     .WithMessage("");
        RuleFor(p => p.Container).NotEmpty().MaximumLength(25);
        RuleFor(p => p.TaskCode).NotEmpty().MaximumLength(50);
    }
}