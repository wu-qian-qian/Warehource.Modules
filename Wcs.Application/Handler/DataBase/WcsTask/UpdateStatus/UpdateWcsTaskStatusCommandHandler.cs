using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.DataBase.WcsTask.UpdateStatus;

internal class UpdateWcsTaskStatusCommandHandler(IWcsTaskRepository _wcsTaskRepositoty, IUnitOfWork _unitofWork)
    : ICommandHandler<UpdateWcsTaskStatusCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateWcsTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<string>();
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