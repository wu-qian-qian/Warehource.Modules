using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Quartz;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Job;
using Wcs.Domain.JobConfigs;

namespace Wcs.Application.DBHandler.Job.SetStatus;

internal class StatusJobEventHandler(
    JobService jobService,
    IUnitOfWork unitOfWork,
    IScheduler scheduler,
    IMapper mapper) : ICommandHandler<StatusJobEvent, Result<JobDto>>
{
    public async Task<Result<JobDto>> Handle(StatusJobEvent request, CancellationToken cancellationToken)
    {
        Result<JobDto> result = new();
        var jobconfig = await jobService.GetJobConfigAsync(request.Name);
        if (jobconfig != null)
        {
            if (request.Status != jobconfig.IsStart)
            {
                if (request.Status)
                {
                    StartJob(scheduler, request.Name);
                    jobconfig.IsStart = true;
                }
                else
                {
                    PauseJob(scheduler, request.Name);
                    jobconfig.IsStart = false;
                }

                await unitOfWork.SaveChangesAsync();
                result.SetValue(mapper.Map<JobDto>(jobconfig));
            }
        }
        else
        {
            result.SetMessage("没有该任务信息");
        }

        return result;
    }


    private static void PauseJob(IScheduler scheduler, string name)
    {
        var jobKey = new JobKey(name);
        scheduler.PauseJob(jobKey);
    }

    private static void StartJob(IScheduler scheduler, string name)
    {
        scheduler.ResumeJob(new JobKey(name));
    }
}