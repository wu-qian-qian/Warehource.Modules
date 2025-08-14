using AutoMapper;
using Common.Application.MediatR.Message;
using Common.Helper;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.DB.WcsTask.Get;

public class GetWcsTaskQueryHandler(
    IWcsTaskRepository _wcsTaskRepository,
    IMapper _mapper) : IQueryHandler<GetWcsTaskQuery, IEnumerable<WcsTaskDto>>
{
    public Task<IEnumerable<WcsTaskDto>> Handle(GetWcsTaskQuery request, CancellationToken cancellationToken)
    {
        var wcsTasks = _wcsTaskRepository.GetWcsTaskQuerys()
            .WhereIf(request.Container != null, p => p.Container == request.Container)
            .WhereIf(request.TaskCode != null, p => p.TaskCode == request.TaskCode)
            .WhereIf(request.StartTime != null, p => p.CreationTime >= request.StartTime)
            .WhereIf(request.EndTime != null, p => p.CreationTime <= request.EndTime)
            .WhereIf(request.SerialNumber != null, p => p.SerialNumber == request.SerialNumber)
            .WhereIf(request.CreatorSystemType != null, p => p.CreatorSystemType == request.CreatorSystemType)
            .Where(p => p.TaskStatus == request.TaskStatus).ToArray();
        return Task.FromResult(_mapper.Map<IEnumerable<WcsTaskDto>>(wcsTasks));
    }
}