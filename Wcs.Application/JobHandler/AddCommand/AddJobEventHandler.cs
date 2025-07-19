using AutoMapper;
using Common.Application.MediatR.Message;
using Common.Application.QuartzJob;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Wcs.Application.Abstract;
using Wcs.Contracts.Job;
using Wcs.Domain.JobConfigs;
using Wcs.Shared;

namespace Wcs.Application.JobHandler.AddCommand;

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
        var jobtype = types.First(x => x.Name == request.JobType);
        QuatrzJobExtensions.CreateJobDetail(jobtype, jobConfig, sc);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<JobDto>(request);
    }
}