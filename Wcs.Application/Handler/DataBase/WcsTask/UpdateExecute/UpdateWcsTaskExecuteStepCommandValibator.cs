using FluentValidation;

namespace Wcs.Application.Handler.DataBase.WcsTask.UpdateExecute;

internal class UpdateWcsTaskExecuteStepCommandValibator : AbstractValidator<UpdateWcsTaskExecuteStepCommand>
{
    public UpdateWcsTaskExecuteStepCommandValibator()
    {
        RuleFor(p => p.DeviceName).NotEmpty();
    }
}