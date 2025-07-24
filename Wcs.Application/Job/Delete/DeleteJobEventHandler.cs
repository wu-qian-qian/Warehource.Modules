using System.Globalization;
using AutoMapper;
using Common.Application.MediatR.Message;
using Quartz;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Job;
using Wcs.Domain.JobConfigs;

namespace Wcs.Application.Job.Delete;

internal class DeleteJobEventHandler(
    JobService jobService,
    IUnitOfWork unitOfWork,
    IScheduler scheduler,
    IMapper mapper) : ICommandHandler<DeleteJobEvent, JobDto>
{
    public async Task<JobDto> Handle(DeleteJobEvent request, CancellationToken cancellationToken)
    {
        var jobconfig = await jobService.DeleteJobConfigAsync(request.Name);
        if (jobconfig != null)
        {
            //只有删除成功才保存数据库
            DeleteJob(scheduler, request.Name);
            await unitOfWork.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                "Job with name {0} does not exist.", request.Name));
        }

        return mapper.Map<JobDto>(request);
    }

    private static void DeleteJob(IScheduler scheduler, string name)
    {
        var jobkey = new JobKey(name);
        scheduler.DeleteJob(jobkey);
    }
}