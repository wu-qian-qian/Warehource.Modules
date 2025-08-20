using UI.Model;

namespace UI.Service;

public class RegionService : IRegionService
{
    public Task<Result<RegionModel[]>> GetRegionsAsync()
    {
        Result<RegionModel[]> result = new();
        var regions = new List<RegionModel>();
        for (var i = 0; i < 5; i++)
            regions.Add(new RegionModel
            {
                Code = $"Region{i}",
                Description = $"Region{i} Description"
            });

        result.Value = regions.ToArray();
        return Task.FromResult(result);
    }

    public async Task<Result<RegionModel>> CreateRegionAsync(RegionModel region)
    {
        Result<RegionModel> result = new();

        return result;
    }
}