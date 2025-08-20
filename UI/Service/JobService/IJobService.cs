using UI.Model;
using UI.Model.Job;

namespace UI.Service.JobService;

public interface IJobService
{
    public Task<Result<JobModel[]>> GetJobsAsync();

    public Task<Result<JobModel>> AddJobAsync(JobModel jobModel);

    public Task<Result<JobModel>> JobStatusAsync(JobModel jobModel);
}