using UI.Model;
using UI.Model.ExecuteNode;

namespace UI.Service.ExecuteNodeService;

public class ExecuteNodeService : IExecuteNodeService
{
    public Task<Result<ExecuteNodeModel[]>> GetExecuteNodesAsync()
    {
        Result<ExecuteNodeModel[]> result = new();
        var executeNodeModels = new List<ExecuteNodeModel>
        {
            new()
            {
                CreationTime = DateTime.Now,
                CurrentDeviceType = "StockIn",
                Index = 1,
                PahtNodeGroup = "1",
                RegionCode = "1"
            },
            new()
            {
                CreationTime = DateTime.Now,
                CurrentDeviceType = "StockIn",
                Index = 1,
                PahtNodeGroup = "1",
                RegionCode = "1"
            }
        };
        result.Value = executeNodeModels.ToArray();
        return Task.FromResult(result);
    }

    public async ValueTask<Result<ExecuteNodeModel>> CreateExecuteNodeAsync(ExecuteNodeModel executeNodeModel)
    {
        var result = new Result<ExecuteNodeModel>();
        return result;
    }
}