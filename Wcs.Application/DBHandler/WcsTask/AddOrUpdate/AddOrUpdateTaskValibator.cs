using FluentValidation;

namespace Wcs.Application.DBHandler.WcsTask.Insert;

public class AddOrUpdateTaskValibator : AbstractValidator<AddOrUpdateWcsTaskEvent>
{
    public AddOrUpdateTaskValibator()
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