using Common.Application.Caching;
using Common.Application.Log;
using Common.Application.MediatR.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.Execute.WritePlcTaskData
{
    internal class WritePlcTaskPlcCommadHandler (ICacheService _cacheService): ICommandHandler<WritePlcTaskdataCommand>
    {
        public async Task Handle(WritePlcTaskdataCommand request, CancellationToken cancellationToken)
        {
            var wcsTask=await _cacheService.GetAsync<WcsTask>(request.Key, cancellationToken);
            if (wcsTask != null)
            {
                wcsTask.TaskExecuteStep.IsSend = false;
            }
            else
            {
                Serilog.Log.Logger.ForCategory(Common.Shared.LogCategory.Business).Information($"缓存失效,{request.Key}，状态更新失败");
            }
        }
    }
}
