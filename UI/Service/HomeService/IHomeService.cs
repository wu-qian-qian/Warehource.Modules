using UI.Model.Home;

namespace UI.Service.HomeService;

public interface IHomeService
{
    public Task<WcsTaskModel[]> GetTasksAsync();
    public Task<ExecuteStepModel[]> GetExecutesAsync();
}