using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Domain.Task;

namespace Wcs.Application.DBHandler.WcsTask.UpdateExecute;

internal class UpdateWcsTaskExecueStepHandler(IWcsTaskRepository _wcsTaskRepositoty)
    : ICommandHandler<UpdateWcsTaskExecuteStepEvent, Result<string>>
{
    public Task<Result<string>> Handle(UpdateWcsTaskExecuteStepEvent request, CancellationToken cancellationToken)
    {
        var wcstask = _wcsTaskRepositoty.Get(request.SerialNumber);
        Result<string> result = new();
        if (wcstask != null)
            result.SetMessage("修改成功");
        else
            result.SetMessage("无该任务");
        return Task.FromResult(result);
    }
}