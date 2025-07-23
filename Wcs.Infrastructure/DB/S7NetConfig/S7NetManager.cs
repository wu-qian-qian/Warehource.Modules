using Common.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Wcs.Domain.S7;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.S7NetConfig;

public class S7NetManager(WCSDBContext context) : IS7NetManager
{
    public Task<List<Domain.S7.S7NetConfig>> GetAllNet()
    {
        return context.Query<Domain.S7.S7NetConfig>()
            .Include(p => p.S7EntityItems)
            .ToListAsync();
    }

    public Task<List<Domain.S7.S7EntityItem>> GetAllNetEntityItem()
    {
        return context.Query<Domain.S7.S7EntityItem>()
            .ToListAsync();
    }
}