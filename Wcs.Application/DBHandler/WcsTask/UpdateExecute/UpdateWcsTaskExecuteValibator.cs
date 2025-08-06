using FluentValidation;

namespace Wcs.Application.DBHandler.WcsTask.UpdateExecute;

internal class UpdateWcsTaskExecuteValibator : AbstractValidator<UpdateWcsTaskExecuteStepEvent>
{
    public UpdateWcsTaskExecuteValibator()
    {
        RuleFor(p => p.DeviceName).NotEmpty();
    }
}