using Wcs.Application.Abstract;
using Wcs.Domain.JobConfigs;

namespace Wcs.Infrastructure.Job.Service;

public class JobService(IJobConfigRepository jobConfigRepository) : IJobService
{
    public async Task<JobConfig> GetJobConfigAsync(Guid id)
    {
        return await jobConfigRepository.GetAsync(id);
    }

    public async Task<IEnumerable<JobConfig>> GetAllJobConfigsAsync()
    {
        return await jobConfigRepository.GetListAsync();
    }

    public async Task AddJobConfigAsync(JobConfig jobConfig)
    {
        await jobConfigRepository.InserAsync(jobConfig);
    }

    public async Task UpdateJobConfigAsync(JobConfig jobConfig)
    {
        await jobConfigRepository.UpdateAsync(jobConfig);
    }

    public async Task DeleteJobConfigAsync(Guid id)
    {
        var jobConfig = await jobConfigRepository.GetAsync(id);
        if (jobConfig != null) jobConfig.SoftDelete();
    }

    /// <summary>
    ///     开启了状态追钟并进行软删除
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<JobConfig?> DeleteJobConfigAsync(string name)
    {
        if ((await jobConfigRepository.GetQueryableAsync()).Any(p => p.Name == name))
        {
            var jobConfig = (await jobConfigRepository.GetQueryableAsync(false)).First(p => p.Name == name);
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
        if ((await jobConfigRepository.GetQueryableAsync()).Any(p => p.Name == name))
        {
            var jobConfig = (await jobConfigRepository.GetQueryableAsync(false)).First(p => p.Name == name);
            return jobConfig;
        }

        return null;
    }
}