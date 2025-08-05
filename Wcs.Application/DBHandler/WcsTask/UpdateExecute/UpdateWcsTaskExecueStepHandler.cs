using Common.Application.MediatR.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Domain.Task;

namespace Wcs.Application.DBHandler.WcsTask.UpdateExecute
{
    internal class UpdateWcsTaskExecueStepHandler(IWcsTaskRepository _wcsTaskRepositoty) : ICommandHandler<UpdateWcsTaskExecuteStepEvent>
    {
        public Task Handle(UpdateWcsTaskExecuteStepEvent request, CancellationToken cancellationToken)
        {
            var wcstask = _wcsTaskRepositoty.Get(request.SerialNumber);
            if (wcstask != null)
            {
                
            }
            return Task.CompletedTask;
        }
    }
}
