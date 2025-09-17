using AutoMapper;
using Common.Application.MediatR.Message;
using Common.Helper;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Domain.Task;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.WcsTask.Get;

public class GetWcsTaskQueryHandler(
    IWcsTaskRepository _wcsTaskRepository,
    IMapper _mapper) : IQueryHandler<GetWcsTaskQuery, IEnumerable<WcsTaskDto>>
{
    public Task<IEnumerable<WcsTaskDto>> Handle(GetWcsTaskQuery request, CancellationToken cancellationToken)
    {
        var wcsTasks = _wcsTaskRepository.GetWcsTaskQuerys()
            .WhereIf(request.Container != null, p => p.Container.Contains(request.Container))
            .WhereIf(request.TaskCode != null, p => p.TaskCode.Contains(request.TaskCode))
            .WhereIf(request.StartTime != null, p => p.CreationTime >= request.StartTime)
            .WhereIf(request.EndTime != null, p => p.CreationTime <= request.EndTime)
            .WhereIf(request.SerialNumber != null, p => p.SerialNumber == request.SerialNumber)
            .WhereIf(request.CreatorSystemType != null, p => p.CreatorSystemType == request.CreatorSystemType)
            .WhereIf(request.TaskType != null, p => p.TaskType == request.TaskType)
            .Where(p => p.TaskStatus != WcsTaskStatusEnum.Completed)
            .Where(p => p.TaskStatus != WcsTaskStatusEnum.Cancelled)
            .ToArray();
        return Task.FromResult(_mapper.Map<IEnumerable<WcsTaskDto>>(wcsTasks));
    }
}