namespace Plc.Domain.S7;

public interface IS7NetManager
{
    public Task<List<S7NetConfig>> GetAllNetAsync();

    public Task<List<S7EntityItem>> GetAllNetEntityItemAsync();

    public Task InsertS7NetAsync(IEnumerable<S7NetConfig> s7NetConfigs);
    
    public Task<S7NetConfig> GetNetWiteIpAsync(string ip);
}