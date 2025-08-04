namespace Wcs.Domain.Region;

public interface IRegionRepository
{
    void Insert(Region region);

    Region Get(Guid id);

    void Update(Region region);

    void Delete(Guid id);

    IEnumerable<Region> GetAllRegions();
}