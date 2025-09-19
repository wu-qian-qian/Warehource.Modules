using AutoMapper;
using Common.Application.MediatR.Message.PageQuery;
using Common.Helper;
using Common.Shared;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Domain.Task;

namespace Wcs.Application.Handler.DataBase.WcsTask.Page;

internal class GetWcsTaskPageCommandHandler(IWcsTaskRepository _wcsTaskRepository, IMapper _mapper)
    : IPageHandler<GetWcsTaskPageCommand, WcsTaskDto>
{
    public Task<PageResult<WcsTaskDto>> Handle(GetWcsTaskPageCommand request, CancellationToken cancellationToken)
    {
        var query = _wcsTaskRepository.GetWcsTaskQuerys()
            .WhereIf(request.Container != null, p => p.Container.Contains(request.Container))
            .WhereIf(request.TaskCode != null, p => p.TaskCode.Contains(request.TaskCode))
            .WhereIf(request.StartTime != null, p => p.CreationTime >= request.StartTime)
            .WhereIf(request.EndTime != null, p => p.CreationTime <= request.EndTime)
            .WhereIf(request.SerialNumber != null, p => p.SerialNumber == request.SerialNumber)
            .WhereIf(request.CreatorSystemType != null, p => p.CreatorSystemType == request.CreatorSystemType)
            .WhereIf(request.TaskStatus != null, p => p.TaskStatus == request.TaskStatus)
            .WhereIf(request.WcsTaskType != null, p => p.TaskType == request.WcsTaskType)
            .WhereIf(request.GetLocation != null
                , p => $"{p.GetLocation.GetTunnel}_{p.GetLocation.GetRow}_{p.GetLocation.GetColumn}_{p.GetLocation.GetFloor}" == request.GetLocation)
            .WhereIf(request.PutLocation != null
                , p => $"{p.PutLocation.PutTunnel}_{p.PutLocation.PutRow}_{p.PutLocation.PutColumn}_{p.PutLocation.PutFloor}" == request.PutLocation);
        var count = query.Count();
        var data = query.ToPageBySortAsc(request.SkipCount, request.Total, p => p.CreationTime).ToList();
        var list = _mapper.Map<List<WcsTaskDto>>(data);
        return Task.FromResult(new PageResult<WcsTaskDto>(count, list));
    }
}