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
            .ForMember(x => x.GetColumn, x => x.MapFrom(x => x.GetLocation.GetColumn))
            .ForMember(x => x.GetRow, x => x.MapFrom(x => x.GetLocation.GetRow))
            .ForMember(x => x.GetDepth, x => x.MapFrom(x => x.GetLocation.GetDepth))
            .ForMember(x => x.GetFloor, x => x.MapFrom(x => x.GetLocation.GetFloor))
            .ForMember(x => x.GetTunnel, x => x.MapFrom(x => x.GetLocation.GetTunnel))
            .ForMember(x => x.PutColumn, x => x.MapFrom(x => x.PutLocation.PutColumn))
            .ForMember(x => x.PutRow, x => x.MapFrom(x => x.PutLocation.PutRow))
            .ForMember(x => x.PutDepth, x => x.MapFrom(x => x.PutLocation.PutDepth))
            .ForMember(x => x.PutFloor, x => x.MapFrom(x => x.PutLocation.PutFloor))
            .ForMember(x => x.PutTunnel, x => x.MapFrom(x => x.PutLocation.PutTunnel))
            .ForMember(x => x.ExecuteDesc, x => x.MapFrom(x => x.TaskExecuteStep.Description))
            .ForMember(x => x.ExecutePath, x => x.MapFrom(x => x.TaskExecuteStep.PathNodeGroup))
            .ForMember(x => x.CurrentDevice, x => x.MapFrom(x => x.TaskExecuteStep.CurentDevice));

        CreateMap<InsertWcsTaskRequest, AddOrUpdateWcsTaskCommand>();

        CreateMap<ExecuteNodePath, ExecuteNodeDto>();

        CreateMap<Domain.Device.Device, DeviceDto>();
    }
}