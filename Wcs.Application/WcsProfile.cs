using AutoMapper;
using Wcs.Application.Handler.DataBase.Job.Insert;
using Wcs.Application.Handler.DataBase.WcsTask.AddOrUpdate;
using Wcs.Contracts.Request.WcsTask;
using Wcs.Contracts.Respon.Device;
using Wcs.Contracts.Respon.ExecuteNode;
using Wcs.Contracts.Respon.Job;
using Wcs.Contracts.Respon.Region;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Domain.ExecuteNode;
using Wcs.Domain.JobConfigs;
using Wcs.Domain.Region;
using Wcs.Domain.Task;

namespace Wcs.Application;

internal class WcsProfile : Profile
{
    public WcsProfile()
    {
        CreateMap<JobConfig, JobDto>();
        CreateMap<AddJobCommand, JobDto>();

        CreateMap<Region, RegionDto>();

        CreateMap<WcsTask, WcsTaskDto>()
            .ForMember(x => x.GetLocation, x =>
                x.MapFrom(x =>
                    $"{x.GetLocation.GetTunnel}_{x.GetLocation.GetRow}_{x.GetLocation.GetColumn}_{x.GetLocation.GetFloor}_{x.GetLocation.GetDepth}"))
            .ForMember(x => x.PutLocation, x =>
                x.MapFrom(x =>
                    $"{x.PutLocation.PutTunnel}_{x.PutLocation.PutRow}_{x.PutLocation.PutColumn}_{x.PutLocation.PutFloor}_{x.PutLocation.PutDepth}"))
            .ForMember(x => x.ExecuteDesc, x => x.MapFrom(x => x.TaskExecuteStep.Description))
            .ForMember(x => x.ExecutePath, x => x.MapFrom(x => x.TaskExecuteStep.PathNodeGroup))
            .ForMember(x => x.CurrentDevice, x => x.MapFrom(x => x.TaskExecuteStep.CurentDevice));

        CreateMap<InsertWcsTaskRequest, AddOrUpdateWcsTaskCommand>();

        CreateMap<ExecuteNodePath, ExecuteNodeDto>();

        CreateMap<Domain.Device.Device, DeviceDto>();
    }
}