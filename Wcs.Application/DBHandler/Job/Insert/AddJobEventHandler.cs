using AutoMapper;
using Common.Application.Exception;
using Common.Application.MediatR.Message;
using Common.Application.QuartzJob;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Job;
using Wcs.Domain.JobConfigs;
using Wcs.Shared;

namespace Wcs.Application.DBHandler.Job.Insert;

internal class AddJobEventHandler(
    JobService jobService,
    IUnitOfWork unitOfWork,
    IServiceProvider serviceProvider,
    IMapper mapper) : ICommandHandler<AddJobEvent, JobDto>
{
    public async Task<JobDto> Handle(AddJobEvent request, CancellationToken cancellationToken)
    {
        var jobConfig = new JobConfig
        {
            Name = request.Name,
            Description = request.Description,
            JobType = request.JobType,
            TimeOut = request.TimeOut,
            Timer = request.Timer,
            IsStart = request.IsStart
        };
        await jobService.AddJobConfigAsync(jobConfig);

        var types = serviceProvider.GetKeyedService<Type[]>(Constant.JobKey);
        var sc = serviceProvider.GetService<IScheduler>();
        if (types.Any(p => p.Name == request.JobType) == false)
            throw new CommonException($"{nameof(AddJobEvent)}--不存在该类型");
        var jobtype = types.First(x => x.Name == request.JobType);
        QuatrzJobExtensions.CreateJobDetail(jobtype, jobConfig, sc);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<JobDto>(request);
    }
}