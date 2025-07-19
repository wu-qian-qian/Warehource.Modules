using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Job;
using JobService = Wcs.Domain.JobConfigs.JobService;

namespace Wcs.Application.JobHandler.GetQuery;

internal sealed class GetJobQueryHandler(JobService jobService, IMapper mapper) : IQueryHandler<GetJobQuery, JobDto>
{
    public async Task<JobDto> Handle(GetJobQuery request, CancellationToken cancellationToken)
    {
        var jobConfig = await jobService.GetJobConfigAsync(request.Id);
        return mapper.Map<JobDto>(jobConfig);
    }
}