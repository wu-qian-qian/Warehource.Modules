using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Quartz;
using Quartz.Impl.Matchers;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Job;
using Wcs.Domain.JobConfigs;

namespace Wcs.Application.Handler.DataBase.Job.Update;

internal class UpdateJobCommandHandler(
    IJobService jobService,
    IUnitOfWork unitOfWork,
    IScheduler scheduler,
    IMapper mapper) : ICommandHandler<UpdateJobCommand, Result<JobDto>>
{
    public async Task<Result<JobDto>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        Result<JobDto> result = new();
        var jobconfig = await jobService.GetJobConfigAsync(request.Name);
        if (jobconfig != null)
        {
            if (request.IsStart.HasValue)
                if (request.IsStart.Value != jobconfig.IsStart)
                {
                    if (request.IsStart.Value)
                    {
                        jobconfig.SetStatus(true);
                        await StartJob(jobconfig);
                    }
                    else
                    {
                        jobconfig.SetStatus(false);
                        await PauseJob(scheduler, request.Name);
                    }
                }

            if (jobconfig.IsStart == false)
            {
                if (request.Timer.HasValue) jobconfig.SetTimer(request.Timer.Value);
                if (request.TimerOut.HasValue) jobconfig.SetTimerOut(request.TimerOut.Value);
                if (request.Description != null) jobconfig.Description = request.Description;
            }

            await unitOfWork.SaveChangesAsync();
            result.SetValue(mapper.Map<JobDto>(jobconfig));
        }
        else
        {
            result.SetMessage("没有该任务信息");
        }

        return result;
    }


    private static async Task PauseJob(IScheduler scheduler, string name)
    {
        var jobKey = new JobKey(name);
        scheduler.PauseJob(jobKey);
    }

    private async Task StartJob(JobConfig config)
    {
        var allJobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
        if (allJobKeys.Any(p => p.Name == config.Name))
            scheduler.ResumeJob(new JobKey(config.Name));
        else
            jobService.CraetJob(config);
    }
}