using Common.Application.Caching;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.RefreshTaskStatus;

/// <summary>
///     将缓存数据更新到数据库
/// </summary>
/// <param name="_wcsTaskRepository"></param>
/// <param name="_cacheService"></param>
/// <param name="_unitOfWork"></param>
public class RefreshTaskStatusCommandHandler(
    IWcsTaskRepository _wcsTaskRepository,
    ICacheService _cacheService,
    IUnitOfWork _unitOfWork) : ICommandHandler<RefreshTaskStatusCommand>
{
    public async Task Handle(RefreshTaskStatusCommand request, CancellationToken cancellationToken)
    {
        //初始该设备的状态
        request.WcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.ToBeSend;
        _wcsTaskRepository.Update(request.WcsTask);
        await _unitOfWork.SaveChangesAsync();
        await _cacheService.RemoveAsync(request.Key);
    }
}