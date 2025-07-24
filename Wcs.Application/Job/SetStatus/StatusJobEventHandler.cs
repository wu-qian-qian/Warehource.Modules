using System.Globalization;
using AutoMapper;
using Common.Application.MediatR.Message;
using Quartz;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Job;
using Wcs.Domain.JobConfigs;

namespace Wcs.Application.Job.SetStatus;

internal class StatusJobEventHandler(
    JobService jobService,
    IUnitOfWork unitOfWork,
    IScheduler scheduler,
    IMapper mapper) : ICommandHandler<StatusJobEvent, JobDto>
{
    public async Task<JobDto> Handle(StatusJobEvent request, CancellationToken cancellationToken)
    {
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
            }
        }
        else
        {
            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                "Job with name {0} does not exist.", request.Name));
        }

        return mapper.Map<JobDto>(jobconfig);
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