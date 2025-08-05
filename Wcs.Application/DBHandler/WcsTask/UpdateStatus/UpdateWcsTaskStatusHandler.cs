using Common.Application.MediatR.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.Abstract;
using Wcs.Domain.Task;

namespace Wcs.Application.DBHandler.WcsTask.UpdateExecute
{
    internal class UpdateWcsTaskStatusHandler(IWcsTaskRepository _wcsTaskRepositoty,IUnitOfWork _unitofWork) : ICommandHandler<UpdateWcsTaskStatusEvent>
    {
        public async Task Handle(UpdateWcsTaskStatusEvent request, CancellationToken cancellationToken)
        {
            var wcstask = _wcsTaskRepositoty.Get(request.SerialNumber);
            if (wcstask != null)
            {
                wcstask.TaskStatus = request.WcsTaskStatusType;
            }
            await _unitofWork.SaveChangesAsync();
        }
    }
}
