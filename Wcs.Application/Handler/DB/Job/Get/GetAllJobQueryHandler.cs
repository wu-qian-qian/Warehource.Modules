using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.Handler.DB.Job.Get;

internal sealed class GetAllJobQueryHandler(IJobService jobService, IMapper mapper)
    : IQueryHandler<GetAllJobQuery, IEnumerable<JobDto>>
{
    public async Task<IEnumerable<JobDto>> Handle(GetAllJobQuery request, CancellationToken cancellationToken)
    {
        var jobs = await jobService.GetAllJobConfigsAsync();
        return mapper.Map<IEnumerable<JobDto>>(jobs);
    }
}