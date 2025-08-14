using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.DBHandler.Job.Get;

internal sealed class GetJobQueryHandler(IJobService jobService, IMapper mapper) : IQueryHandler<GetJobQuery, JobDto>
{
    public async Task<JobDto> Handle(GetJobQuery request, CancellationToken cancellationToken)
    {
        var jobConfig = await jobService.GetJobConfigAsync(request.Id);
        return mapper.Map<JobDto>(jobConfig);
    }
}