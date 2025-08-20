using UI.Model;
using UI.Model.Home;

namespace UI.Service.HomeService;

public class HomeService : IHomeService
{
    public readonly IHttpClientFactory _httpFactory;

    public Task<Result<WcsTaskModel[]>> GetTasksAsync()
    {
        Result<WcsTaskModel[]> result = new();
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
        result.Value = data.ToArray();
        return Task.FromResult(result);
    }
}