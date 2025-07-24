namespace Wcs.Contracts.Request.Job;

public record AddJobRequest(
    string Name,
    string Description,
    string Jobtype,
    int TimeOut = 5000,
    int Timer = 3,
    bool IsStart = false);