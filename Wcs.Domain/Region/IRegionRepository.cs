namespace Wcs.Domain.Region;

public interface IRegionRepository
{
    void Insert(Region region);

    Region Get(Guid id);

    Region Get(string regionCode);

    void Update(Region region);

    void Delete(Guid id);

    IEnumerable<Region> GetAllRegions();

    IQueryable<Region> GetQuery();
}