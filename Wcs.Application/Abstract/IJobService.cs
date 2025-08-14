using Wcs.Domain.JobConfigs;

namespace Wcs.Application.Abstract;

public interface IJobService
{
    public Task<JobConfig> GetJobConfigAsync(Guid id);

    public Task<IEnumerable<JobConfig>> GetAllJobConfigsAsync();

    public Task AddJobConfigAsync(JobConfig jobConfig);

    public Task UpdateJobConfigAsync(JobConfig jobConfig);

    public Task DeleteJobConfigAsync(Guid id);

    public Task<JobConfig?> DeleteJobConfigAsync(string name);

    public Task<JobConfig?> GetJobConfigAsync(string name);
}