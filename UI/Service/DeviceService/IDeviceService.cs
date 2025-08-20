using UI.Model;
using UI.Model.Device;

namespace UI.Service.DeviceService;

public interface IDeviceService
{
    Task<Result<List<DeviceModel>>> GetDeviceListAsync();

    Task<Result<DeviceModel>> CreatDeviceAsync(DeviceRequest request);
}