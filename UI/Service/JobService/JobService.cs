using UI.Model;
using UI.Model.Job;

namespace UI.Service.JobService;

public class JobService : IJobService
{
    public Task<Result<JobModel[]>> GetJobsAsync()
    {
        Result<JobModel[]> result = new();
        var jobs = new List<JobModel>();
        jobs.Add(
            new JobModel
            {
                Name = "RealJob",
                JobType = "RealPlcJob",
                Timer = 2000,
                TimeOut = 8000,
                CreationTime = DateTime.Now,
                Description = "用来读取Plc",
                IsStart = false
            });
        result.Value = jobs.ToArray();
        return Task.FromResult(result);
    }

    public Task<Result<JobModel>> AddJobAsync(JobModel jobModel)
    {
        throw new NotImplementedException();
    }

    public Task<Result<JobModel>> JobStatusAsync(JobModel jobModel)
    {
        throw new NotImplementedException();
    }
}