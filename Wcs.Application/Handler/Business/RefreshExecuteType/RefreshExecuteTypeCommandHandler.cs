using Common.Application.Caching;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.RefreshExecuteType;

/// <summary>
///     将缓存数据更新到数据库
/// </summary>
/// <param name="_wcsTaskRepository"></param>
/// <param name="_cacheService"></param>
/// <param name="_unitOfWork"></param>
public class RefreshExecuteTypeCommandHandler(
    IWcsTaskRepository _wcsTaskRepository,
    ICacheService _cacheService,
    IUnitOfWork _unitOfWork) : ICommandHandler<RefreshExecuteTypeCommand>
{
    public async Task Handle(RefreshExecuteTypeCommand request, CancellationToken cancellationToken)
    {
        var wcsTask = await _cacheService.GetAsync<WcsTask>(request.Key);
        if (wcsTask != null)
        {
            //初始该设备的状态
            wcsTask.TaskExecuteStep.TaskExecuteStepType = TaskExecuteStepTypeEnum.ToBeSend;
            _wcsTaskRepository.Update(wcsTask);
            await _unitOfWork.SaveChangesAsync();
            await _cacheService.RemoveAsync(request.Key);
        }
    }
}