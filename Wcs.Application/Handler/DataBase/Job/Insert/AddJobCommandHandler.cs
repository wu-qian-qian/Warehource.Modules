using AutoMapper;
using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Contracts.Respon.Job;
using Wcs.Domain.JobConfigs;

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
        var @bool = await jobService.AddJobConfigAsync(jobConfig);
        if (@bool)
        {
            jobService.CraetJob(jobConfig);
            await unitOfWork.SaveChangesAsync();
            result.SetValue(mapper.Map<JobDto>(request));
        }
        else
        {
            result.SetMessage("任务类型已存在无法重复添加");
        }

        return result;
    }
}