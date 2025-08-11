using UI.Model.Home;

namespace UI.Service.HomeService;

public class HomeService : IHomeService
{
    public readonly IHttpClientFactory _httpFactory;

    public Task<WcsTaskModel[]> GetTasksAsync()
    {
        var data = Enumerable.Range(0, 100).Select(i => new WcsTaskModel
        {
            TaskCode = $"Edrward {i}",
            SerialNumber = i,
            Description = $"London Park no. {i}",
            TaskType = $"Task Type {i}",
            TaskStatus = $"Task Status {i}",
            CreationTime = DateTime.Now.AddDays(-i),
            ExecuteDesc = $"Execute Step {i}"
        });
        return Task.FromResult(data.ToArray());
    }

    public Task<ExecuteStepModel[]> GetExecutesAsync()
    {
        var data = Enumerable.Range(0, 5).Select(i => new ExecuteStepModel
        {
            ExecuteName = $"执行--{i}",
            ExecuteStep = $"{i}"
        });
        return Task.FromResult(data.ToArray());
    }
}