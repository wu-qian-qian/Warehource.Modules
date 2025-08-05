using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcs.Application.DBHandler.WcsTask.UpdateExecute
{
    internal class UpdateWcsTaskExecuteValibator : AbstractValidator<UpdateWcsTaskExecuteStepEvent>
    {
        public UpdateWcsTaskExecuteValibator()
        {
            RuleFor(p => p.DeviceName).NotEmpty();
        }
    }
}
