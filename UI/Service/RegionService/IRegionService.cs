using UI.Model;
using UI.Model.Region;

namespace UI.Service.RegionService;

public interface IRegionService
{
    Task<Result<RegionModel[]>> GetRegionsAsync();

    Task<Result<RegionModel>> CreateRegionAsync(RegionModel region);
}