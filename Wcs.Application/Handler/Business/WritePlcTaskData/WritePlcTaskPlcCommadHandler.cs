using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using Common.Shared;
using Serilog;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.WritePlcTaskData;

internal class WritePlcTaskPlcCommadHandler(ICacheService _cacheService) : ICommandHandler<WritePlcTaskdataCommand>
{
    public async Task Handle(WritePlcTaskdataCommand request, CancellationToken cancellationToken)
    {
        var wcsTask = await _cacheService.GetAsync<WcsTask>(request.Key, cancellationToken);
        if (wcsTask != null)
        {
            if (request.IsSucess)
                //更新数据库 TODO
                wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.Complate;
            else
                //更新数据库 TODO
                wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.SendEnding;

            await _cacheService.SetAsync(request.Key, wcsTask);
        }
        else
        {
            Log.Logger.ForCategory(LogCategory.Business).Information($"缓存失效,{request.Key}，状态更新失败");
        }
    }
}