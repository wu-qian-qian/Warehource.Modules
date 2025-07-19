using AutoMapper;
using Wcs.Application.JobHandler.AddCommand;
using Wcs.Contracts.Job;
using Wcs.Domain.JobConfigs;

namespace Wcs.Application;

internal class WcsProfile : Profile
{
    public WcsProfile()
    {
        CreateMap<JobConfig, JobDto>();
        CreateMap<AddJobEvent, JobDto>();
    }
}