using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Common.Application.QuartzJob;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Job;
using Wcs.Domain.JobConfigs;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.Job.Insert;

internal class AddJobCommandHandler(
    IJobService jobService,
    IUnitOfWork unitOfWork,
    IServiceProvider serviceProvider,
    IMapper mapper) : ICommandHandler<AddJobCommand, Result<JobDto>>
{
    public async Task<Result<JobDto>> Handle(AddJobCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<JobDto>();
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
        {
            result.SetMessage($"{nameof(AddJobCommand)}--不存在该类型");
        }
        else
        {
            var jobtype = types.First(x => x.Name == request.JobType);
            QuatrzJobExtensions.CreateJobDetail(jobtype, jobConfig, sc);
            await unitOfWork.SaveChangesAsync();
            result.SetValue(mapper.Map<JobDto>(request));
        }

        return result;
    }
}