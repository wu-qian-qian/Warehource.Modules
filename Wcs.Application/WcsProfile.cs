using AutoMapper;
using Wcs.Application.DBHandler.Job.Insert;
using Wcs.Contracts.Respon.Job;
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