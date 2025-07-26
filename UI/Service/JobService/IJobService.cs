using UI.Model.Job;

namespace UI.Service.JobService;

public interface IJobService
{
    public Task<JobModel[]> GetJobsAsync();

    public Task<JobModel> AddJobAsync(JobModel jobModel);

    public Task<JobModel> JobStatusAsync(JobModel jobModel);
}