using FluentValidation;

namespace Wcs.Application.Handler.DB.WcsTask.UpdateExecute;

internal class UpdateWcsTaskExecuteStepCommandValibator : AbstractValidator<UpdateWcsTaskExecuteStepCommand>
{
    public UpdateWcsTaskExecuteStepCommandValibator()
    {
        RuleFor(p => p.DeviceName).NotEmpty();
    }
}