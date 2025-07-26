using UI.Model.Job;

namespace UI.Service.JobService;

public class JobService : IJobService
{
    public Task<JobModel[]> GetJobsAsync()
    {
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
        return Task.FromResult(jobs.ToArray());
    }

    public Task<JobModel> AddJobAsync(JobModel jobModel)
    {
        throw new NotImplementedException();
    }

    public Task<JobModel> JobStatusAsync(JobModel jobModel)
    {
        throw new NotImplementedException();
    }
}