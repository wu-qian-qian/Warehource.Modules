using Common.Infrastructure.EF.Repository;
using Wcs.Domain.JobConfigs;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.JobConfig;

public class JobConfigRepository : EfCoreRepository<Domain.JobConfigs.JobConfig, WCSDBContext>, IJobConfigRepository
{
    public JobConfigRepository(WCSDBContext dbContext) : base(dbContext)
    {
    }
}