using Common.Application.MediatR.Message;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.FilterStackerTask;

public class FilterStackerCommandHandler : ICommandHandler<FilterStackerCommand, WcsTask>
{
    public Task<WcsTask> Handle(FilterStackerCommand request, CancellationToken cancellationToken)
    {
        var wcsTasks = request.WcsTasks;
        var tempWcsTasks = wcsTasks.Where(p => p.IsEnforce);
        if (tempWcsTasks.Any() == false)
        {
            tempWcsTasks = wcsTasks.Where(p => p.TaskType == WcsTaskTypeEnum.StockMove);
            if (tempWcsTasks.Any() == false)
                if (request.IsTranShipPoint)
                    tempWcsTasks = wcsTasks.Where(p => p.TaskType == WcsTaskTypeEnum.StockIn);
                else
                    tempWcsTasks = wcsTasks.Where(p => p.TaskType == WcsTaskTypeEnum.StockOut);
        }

        if (tempWcsTasks.Any() == false) tempWcsTasks = wcsTasks;
        tempWcsTasks = tempWcsTasks.OrderBy(p => p.Level).ThenBy(p => p.CreationTime);
        return Task.FromResult(tempWcsTasks.First());
    }
}