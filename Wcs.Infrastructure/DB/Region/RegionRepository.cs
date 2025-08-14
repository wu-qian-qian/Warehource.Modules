using Common.Infrastructure.EF;
using Wcs.Domain.Region;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.Region;

public sealed class RegionRepository(WCSDBContext _db) : IRegionRepository
{
    public void Insert(Domain.Region.Region region)
    {
        _db.Regions.Add(region);
    }

    public Domain.Region.Region Get(Guid id)
    {
        if (_db.Regions.Any(p => p.Id == id)) return _db.Regions.First(p => p.Id == id);
        return default;
    }

    public Domain.Region.Region Get(string regionCode)
    {
        if (_db.Regions.Any(p => p.Code == regionCode)) return _db.Regions.First(p => p.Code == regionCode);
        return default;
    }

    public void Update(Domain.Region.Region region)
    {
        _db.Regions.Add(region);
    }

    public void Delete(Guid id)
    {
        if (_db.Regions.Any(p => p.Id == id))
        {
            var region = _db.Regions.First(p => p.Id == id);
            _db.Regions.Remove(region);
        }
    }

    public IEnumerable<Domain.Region.Region> GetAllRegions()
    {
        return _db.Query<Domain.Region.Region>().ToList();
    }

    public IQueryable<Domain.Region.Region> GetQuery()
    {
        return _db.Query<Domain.Region.Region>();
    }
}