using MediatR;
using Wcs.Application.Handler.Execute.GetWcsTask;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Execute.Behaviors.GetTask;

public class GetWcsTaskPipelinBehavior
    : IPipelineBehavior<GetWcsTaskQuery, IEnumerable<WcsTask>>
{
    public async Task<IEnumerable<WcsTask>> Handle(GetWcsTaskQuery request,
        RequestHandlerDelegate<IEnumerable<WcsTask>> next, CancellationToken cancellationToken)
    {
        //使用中间管道进行配置筛选出优先执行的任务
        //如果有特殊配置可以自定义配置  如添加入库优先，或者其他，只需要配置中间管道既可以
        var wcsTasks = await next();
        var tempWcsTasks = wcsTasks.Where(p => p.IsEnforce);
        if (tempWcsTasks.Any() == false)
        {
            tempWcsTasks = wcsTasks.Where(p => p.TaskType == WcsTaskTypeEnum.StockMove);
            if (tempWcsTasks.Any() == false)
                if (request.IsTranShipPoint)
                    tempWcsTasks = wcsTasks.Where(p => p.TaskType == WcsTaskTypeEnum.StockIn);
        }

        if (tempWcsTasks.Any() == false) tempWcsTasks = wcsTasks;
        tempWcsTasks = tempWcsTasks.OrderBy(p => p.Level).ThenBy(p => p.CreationTime);
        return tempWcsTasks;
    }
}