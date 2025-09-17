using Common.Application.QuartzJob;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Wcs.Application.Abstract;
using Wcs.Domain.JobConfigs;
using Wcs.Shared;

namespace Wcs.Infrastructure.Job.Service;

public class JobService(IJobConfigRepository _jobConfigRepository, IServiceProvider serviceProvider) : IJobService
{
    public async Task<JobConfig> GetJobConfigAsync(Guid id)
    {
        return await _jobConfigRepository.GetAsync(id);
    }

    public async Task<IEnumerable<JobConfig>> GetAllJobConfigsAsync()
    {
        return await _jobConfigRepository.GetListAsync();
    }

    public async Task<bool> AddJobConfigAsync(JobConfig jobConfig)
    {
        var entity =
            (await _jobConfigRepository.GetQueryableAsync()).FirstOrDefault(p => p.JobType == jobConfig.JobType);
        if (entity == null)
        {
            await _jobConfigRepository.InserAsync(jobConfig);
            return true;
        }

        return false;
    }

    public async Task UpdateJobConfigAsync(JobConfig jobConfig)
    {
        await _jobConfigRepository.UpdateAsync(jobConfig);
    }

    public async Task DeleteJobConfigAsync(Guid id)
    {
        var jobConfig = await _jobConfigRepository.GetAsync(id);
        if (jobConfig != null) jobConfig.SoftDelete();
    }

    /// <summary>
    ///     开启了状态追钟并进行软删除
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<JobConfig?> DeleteJobConfigAsync(string name)
    {
        if ((await _jobConfigRepository.GetQueryableAsync()).Any(p => p.Name == name))
        {
            var jobConfig = (await _jobConfigRepository.GetQueryableAsync(false)).First(p => p.Name == name);
            if (jobConfig != null) jobConfig.SoftDelete();

            return jobConfig;
        }

        return null;
    }

    /// <summary>
    ///     通过名称获取任务并开启状态追踪
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<JobConfig?> GetJobConfigAsync(string name)
    {
        if ((await _jobConfigRepository.GetQueryableAsync()).Any(p => p.Name == name))
        {
            var jobConfig = (await _jobConfigRepository.GetQueryableAsync(false)).First(p => p.Name == name);
            return jobConfig;
        }

        return null;
    }

    public bool CraetJob(JobConfig jobConfig)
    {
        var types = serviceProvider.GetKeyedService<Type[]>(Constant.JobKey);
        var sc = serviceProvider.GetService<IScheduler>();
        if (types.Any(p => p.Name == jobConfig.JobType) == false)
        {
            return false;
        }

        var jobtype = types.First(x => x.Name == jobConfig.JobType);
        QuatrzJobExtensions.CreateJobDetail(jobtype, jobConfig, sc);
        return true;
    }

    public async Task<IQueryable<JobConfig>> GetQueryableAsync(bool asNoTrack = true)
    {
        return await _jobConfigRepository.GetQueryableAsync(asNoTrack);
    }
}