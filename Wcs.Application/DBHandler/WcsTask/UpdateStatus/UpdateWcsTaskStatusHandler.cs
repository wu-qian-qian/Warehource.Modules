using Common.Application.MediatR.Behaviors;
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
    internal class UpdateWcsTaskStatusHandler(IWcsTaskRepository _wcsTaskRepositoty,IUnitOfWork _unitofWork) : ICommandHandler<UpdateWcsTaskStatusEvent, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateWcsTaskStatusEvent request, CancellationToken cancellationToken)
        {
            Result<string> result = new Result<string>();
            var wcstask = _wcsTaskRepositoty.Get(request.SerialNumber);
            if (wcstask != null)
            {
                wcstask.TaskStatus = request.WcsTaskStatusType;
                await _unitofWork.SaveChangesAsync();
                result.SetMessage("成功");
            }
            else
            {
                result.SetMessage("无任务");
            }
            return result;
        }
    }
}
