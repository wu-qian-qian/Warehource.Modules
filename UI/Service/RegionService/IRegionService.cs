using UI.Model;

namespace UI.Service;

public interface IRegionService
{
    Task<Result<RegionModel[]>> GetRegionsAsync();

    Task<Result<RegionModel>> CreateRegionAsync(RegionModel region);
}