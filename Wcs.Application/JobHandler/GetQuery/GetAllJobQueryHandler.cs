using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Job;
using Wcs.Domain.JobConfigs;

namespace Wcs.Application.JobHandler.GetQuery;

internal sealed class GetAllJobQueryHandler(JobService jobService, IMapper mapper)
    : IQueryHandler<GetAllJobQuery, IEnumerable<JobDto>?>
{
    public async Task<IEnumerable<JobDto>?> Handle(GetAllJobQuery request, CancellationToken cancellationToken)
    {
        var jobs = await jobService.GetAllJobConfigsAsync();
        return mapper.Map<IEnumerable<JobDto>>(jobs);
    }
}