namespace Plc.Domain.S7;

public interface IS7NetManager
{
    public Task<S7NetConfig> GetNetWiteIdAsync(Guid id);

    public Task<S7EntityItem> GetEntityItemAsync(Guid id);
    public Task<List<S7NetConfig>> GetAllNetAsync();

    public Task<List<S7EntityItem>> GetAllNetEntityItemAsync();

    public Task InsertS7NetAsync(IEnumerable<S7NetConfig> s7NetConfigs);

    public Task<S7NetConfig> GetNetWiteIpAsync(string ip);

    public Task<List<S7EntityItem>> GetNetWiteDeviceNameAsync(string deviceName);

    public Task<List<S7EntityItem>?> GetNetWiteDeviceNameAsync(string deviceName, bool isUse);

    public Task<List<S7EntityItem>> GetDeviceNameWithDBNameAsync(string deviceName, List<string> dbNames);

    public void UpdateS7EntityItem(IEnumerable<S7EntityItem> entityItems);

    public void UpdateS7Net(IEnumerable<S7NetConfig> nets);

    public IQueryable<S7NetConfig> GetQueryNetConfig();

    public IQueryable<S7EntityItem> GetQueryS7EntityItem();

    public Task DeleteAsync(S7NetConfig[] s7NetConfigs);
}