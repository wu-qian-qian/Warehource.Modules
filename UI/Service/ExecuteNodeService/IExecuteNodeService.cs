using UI.Model;
using UI.Model.ExecuteNode;

namespace UI.Service.ExecuteNodeService;

public interface IExecuteNodeService
{
    public Task<Result<ExecuteNodeModel[]>> GetExecuteNodesAsync();

    public ValueTask<Result<ExecuteNodeModel>> CreateExecuteNodeAsync(ExecuteNodeModel executeNodeModel);
}