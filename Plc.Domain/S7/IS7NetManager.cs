namespace Plc.Domain.S7;

public interface IS7NetManager
{
    public Task<List<S7NetConfig>> GetAllNet();

    public Task<List<S7EntityItem>> GetAllNetEntityItem();

    public Task InsertS7Net(IEnumerable<S7NetConfig> s7NetConfigs);
}