using UI.Model;
using UI.Model.Home;

namespace UI.Service.HomeService;

public interface IHomeService
{
    public Task<Result<WcsTaskModel[]>> GetTasksAsync();
}