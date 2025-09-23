using Common.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Plc.Domain.S7;
using Plc.Infrastructure.Database;

namespace Plc.Infrastructure.db;

public class S7NetManager(PlcDBContext context) : IS7NetManager
{
    public Task<S7NetConfig> GetNetWiteIdAsync(Guid id)
    {
        return context.Query<S7NetConfig>().Include(p => p.S7EntityItems).FirstAsync(p => p.Id == id);
    }

    public Task<List<S7NetConfig>> GetAllNetAsync()
    {
        return context.Query<S7NetConfig>()
            .ToListAsync();
    }

    public Task<List<S7EntityItem>> GetAllNetEntityItemAsync()
    {
        return context.Query<S7EntityItem>()
            .ToListAsync();
    }

    public Task InsertS7NetAsync(IEnumerable<S7NetConfig> s7NetConfigs)
    {
        return context.Nets.AddRangeAsync(s7NetConfigs);
    }

    public async Task<S7NetConfig?> GetNetWiteIpAsync(string ip)
    {
        return await context.Query<S7NetConfig>()
            .Include(p => p.S7EntityItems)
            .FirstOrDefaultAsync(p => p.Ip == ip);
    }

    public Task<List<S7EntityItem>?> GetNetWiteDeviceNameAsync(string deviceName)
    {
        var netconfig = context.Query<S7EntityItem>()
            .Where(p => p.DeviceName == deviceName && p.IsUse == true)
            .ToListAsync();
        return netconfig;
    }

    public Task<List<S7EntityItem>?> GetNetWiteDeviceNameAsync(string deviceName, bool isUse)
    {
        var netconfig = context.Query<S7EntityItem>()
            .Where(p => p.DeviceName == deviceName)
            .ToListAsync();
        return netconfig;
    }

    public Task<List<S7EntityItem>> GetDeviceNameWithDBNameAsync(string deviceName, List<string> dbNames)
    {
        var netconfig = context.Query<S7EntityItem>()
            .Where(p => p.DeviceName == deviceName && dbNames.Contains(p.Name) && p.IsUse == true)
            .ToListAsync();
        return netconfig;
    }


    public void UpdateS7EntityItem(IEnumerable<S7EntityItem> entityItems)
    {
        context.UpdateRange(entityItems);
    }


    public IQueryable<S7NetConfig> GetQueryNetConfig()
    {
        return context.Query<S7NetConfig>().Include(p => p.S7EntityItems).AsNoTracking();
    }

    public IQueryable<S7EntityItem> GetQueryS7EntityItem()
    {
        return context.Query<S7EntityItem>().AsNoTracking();
    }

    public Task DeleteAsync(S7NetConfig[] s7NetConfigs)
    {
        for (var i = 0; i < s7NetConfigs.Count(); i++)
        {
            s7NetConfigs[i].SoftDelete();
            foreach (var item in s7NetConfigs[i].S7EntityItems) item.SoftDelete();
        }

        UpdateS7Net(s7NetConfigs);
        return Task.CompletedTask;
    }

    public void UpdateS7Net(IEnumerable<S7NetConfig> nets)
    {
        context.Nets.UpdateRange(nets);
    }

    public Task<S7EntityItem> GetEntityItemAsync(Guid id)
    {
        var entity = context.S7Entitys.First(p => p.Id == id);
        return Task.FromResult(entity);
    }
}