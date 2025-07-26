using Common.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Plc.Domain.S7;
using Plc.Infrastructure.Database;

namespace Plc.Infrastructure.S7Net;

public class S7NetManager(PlcDBContext context) : IS7NetManager
{
    public Task<List<S7NetConfig>> GetAllNetAsync()
    {
        return context.Query<S7NetConfig>()
            .Include(p => p.S7EntityItems)
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

    public async Task<S7NetConfig> GetNetWiteIpAsync(string ip)
    {
        return await context.Query<S7NetConfig>()
            .Include(p=>p.S7EntityItems)
            .FirstOrDefaultAsync(p => p.Ip == ip);
    }
}