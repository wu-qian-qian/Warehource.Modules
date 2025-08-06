using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Quartz;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Job;
using Wcs.Domain.JobConfigs;

namespace Wcs.Application.DBHandler.Job.Delete;

internal class DeleteJobEventHandler(
    JobService jobService,
    IUnitOfWork unitOfWork,
    IScheduler scheduler,
    IMapper mapper) : ICommandHandler<DeleteJobEvent, Result<JobDto>>
{
    public async Task<Result<JobDto>> Handle(DeleteJobEvent request, CancellationToken cancellationToken)
    {
        Result<JobDto> result = new();
        var jobconfig = await jobService.DeleteJobConfigAsync(request.Name);
        if (jobconfig != null)
        {
            //只有删除成功才保存数据库
            DeleteJob(scheduler, request.Name);
            await unitOfWork.SaveChangesAsync();
            result.SetValue(mapper.Map<JobDto>(request));
        }
        else
        {
            result.SetMessage("没有任务信息删除失败");
        }

        return result;
    }

    private static void DeleteJob(IScheduler scheduler, string name)
    {
        var jobkey = new JobKey(name);
        scheduler.DeleteJob(jobkey);
    }
}