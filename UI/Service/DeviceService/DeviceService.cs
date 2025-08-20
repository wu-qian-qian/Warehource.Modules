using UI.Model;
using UI.Model.Device;

namespace UI.Service.DeviceService;

public class DeviceService : IDeviceService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DeviceService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Task<Result<List<DeviceModel>>> GetDeviceListAsync()
    {
        var result = new Result<List<DeviceModel>>();
        var list = new List<DeviceModel>();
        for (var i = 0; i < 10; i++)
            list.Add(
                new DeviceModel
                {
                    DeviceName = $"DeviceName{i}",
                    DeviceType = $"DeviceType{i}",
                    Description = $"Description{i}",
                    Config = $"Config{i}",
                    RegionCode = $"RegionCode{i}"
                }
            );
        result.Value = list;
        return Task.FromResult(result);
    }

    public async Task<Result<DeviceModel>> CreatDeviceAsync(DeviceRequest request)
    {
        var result = new Result<DeviceModel>();
        result.Message = "成功";
        result.IsSuccess = true;
        return result;
    }
}