namespace Wcs.Contracts.Respon.Job;

public sealed class JobDto : BaseDto
{
    // Primary constructor  
    public JobDto(Guid id, string name, string description, string jobType, int timeOut, int timer, bool isStart)
    {
        Id = id;
        Name = name;
        Description = description;
        JobType = jobType;
        TimeOut = timeOut;
        Timer = timer;
        IsStart = isStart;
    }

    // Parameterless constructor using 'this' initializer  
    public JobDto()
    {
    }

    public string Name { get; init; }
    public string Description { get; init; }
    public string JobType { get; init; }
    public int TimeOut { get; init; }
    public int Timer { get; init; }
    public bool IsStart { get; init; }
}