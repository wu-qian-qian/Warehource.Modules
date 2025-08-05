using AutoMapper;
using Wcs.Application.DBHandler.Job.Insert;
using Wcs.Application.DBHandler.WcsTask.Insert;
using Wcs.Contracts.Request.WcsTask;
using Wcs.Contracts.Respon.Job;
using Wcs.Contracts.Respon.Region;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Domain.JobConfigs;
using Wcs.Domain.Region;
using Wcs.Domain.Task;

namespace Wcs.Application;

internal class WcsProfile : Profile
{
    public WcsProfile()
    {
        CreateMap<JobConfig, JobDto>();
        CreateMap<AddJobEvent, JobDto>();

        CreateMap<Region, RegionDto>();

        CreateMap<WcsTask, WcsTaskDto>();
        
        CreateMap<InsertWcsTaskRequest,AddOrUpdateWcsTaskEvent>();
    }
}