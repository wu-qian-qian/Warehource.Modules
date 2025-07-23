using AutoMapper;
using Wcs.Application.JobHandler.AddCommand;
using Wcs.Contracts.Job;
using Wcs.Contracts.S7Plc;
using Wcs.Domain.JobConfigs;
using Wcs.Domain.S7;

namespace Wcs.Application;

internal class WcsProfile : Profile
{
    public WcsProfile()
    {
        CreateMap<JobConfig, JobDto>();
        CreateMap<AddJobEvent, JobDto>();
        CreateMap<S7NetConfig, S7NetDto>();
        CreateMap<S7EntityItem, S7EntityItemDto>();
    }
}