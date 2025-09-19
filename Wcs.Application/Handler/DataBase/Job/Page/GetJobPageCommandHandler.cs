using AutoMapper;
using Common.Application.MediatR.Message.PageQuery;
using Common.Helper;
using Common.Shared;
using Wcs.Contracts.Respon.Job;
using Wcs.Domain.JobConfigs;

namespace Wcs.Application.Handler.DataBase.Job.Page;

internal class GetJobPageCommandHandler(IJobConfigRepository _jobConfigRepositoryice, IMapper _mapper)
    : IPageHandler<GetJobPageCommand, JobDto>
{
    public async Task<PageResult<JobDto>> Handle(GetJobPageCommand request, CancellationToken cancellationToken)
    {
        var query = (await _jobConfigRepositoryice.GetQueryableAsync())
            .WhereIf(request.Name != null, p => p.Name == request.Name)
            .WhereIf(request.JobType != null, p => p.JobType == request.JobType);
        var count = query.Count();
        var data = query.ToPageBySortAsc(request.SkipCount, request.Total, p => p.CreationTime).AsEnumerable();
        var list = _mapper.Map<List<JobDto>>(data);
        return new PageResult<JobDto>(count, list);
    }
}